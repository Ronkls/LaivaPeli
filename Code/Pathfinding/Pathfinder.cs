using GA.Collections;
using Godot;
using System;
using System.Collections.Generic;
using Cell = GA.Ships.Pathfinding.NavigationGrid.Cell;

namespace GA.Ships.Pathfinding
{
    public class Pathfinder
    {
        private NavigationGrid _grid = null;

        public Pathfinder(NavigationGrid grid)
        {
            _grid = grid;
        }

        /// <summary>
        /// Performs a breadth-first search to find a path from the start position to the end position.
        /// Link: https://en.wikipedia.org/wiki/Breadth-first_search
        /// </summary>
        /// <param name="startPosition">Start position</param>
        /// <param name="endPosition">End position</param>
        /// <returns>List of positions representing the path, or null if no path is found</returns>
        public IList<Vector3> BreadthFirstSearch(Vector3 startPosition, Vector3 endPosition)
        {
            Queue<Cell> frontier = new Queue<Cell>();
            Dictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();

            Cell startCell = _grid.GetCell(startPosition);
            Cell endCell = _grid.GetCell(endPosition);

            frontier.Enqueue(startCell);
            cameFrom[startCell] = null;

            bool isEndReached = false; // Early exit flag

            while (frontier.Count > 0)
            {
                Cell current = frontier.Dequeue();

                isEndReached = current == endCell;
                if (isEndReached)
                {
                    // The end node is reached. Path is complete.
                    break;
                }

                IList<Cell> neighbours = _grid.GetNeighbours(current, includeDiagonal: false);
                foreach (Cell neighbour in neighbours)
                {
                    if (neighbour.IsWalkable && !cameFrom.ContainsKey(neighbour))
                    {
                        frontier.Enqueue(neighbour);
                        cameFrom[neighbour] = current;
                    }
                }
            }

            // If isEndReached is false here, there is no path to the end cell.
            if (isEndReached)
            {
                // Construct path
                return ConstructPath(startCell, endCell, cameFrom);
            }

            // There is no path between start and end positions.
            return null;
        }

        private IList<Vector3> ConstructPath(Cell startCell, Cell endCell, Dictionary<Cell, Cell> cameFrom)
        {
            IList<Vector3> path = new List<Vector3>();
            Cell current = endCell;

            while (current != startCell)
            {
                path.Add(current.WorldPosition);
                current = cameFrom[current];
            }

            path.Reverse();

            return path;
        }

        /// <summary>
        /// Finds all walkable cells that can be reached from the start cell within the given number of steps.
        /// </summary>
        /// <param name="start">The cell to start from.</param>
        /// <param name="maxSteps">The maximum number of steps from the start cell.</param>
        /// <returns>A list of all reachable cells, excluding the start cell.</returns>
        public IList<Cell> GetReachableCells(Cell start, int maxSteps)
        {
            if (start == null)
            {
                throw new ArgumentNullException(nameof(start));
            }

            if (!start.IsWalkable)
            {
                throw new ArgumentException("Start cell must be walkable.", nameof(start));
            }

            if (maxSteps < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxSteps));
            }

            Queue<Cell> frontier = new Queue<Cell>();
            Dictionary<Cell, int> steps = new Dictionary<Cell, int>();
            IList<Cell> reachableCells = new List<Cell>(); // Stores all cells that can be reached within maxSteps.

            frontier.Enqueue(start);
            steps[start] = 0;

            // Continue searching while there are cells left to check.
            while (frontier.Count > 0)
            {
                Cell current = frontier.Dequeue();

                // Stop searching from this cell if we have reached the maximum range.
                if (steps[current] >= maxSteps)
                {
                    continue;
                }

                // Get all the walkable cells next to the current cell.
                IList<Cell> neighbours = _grid.GetNeighbours(current, includeDiagonal: false);

                foreach (Cell neighbour in neighbours)
                {
                    // Skips the cell if it has already been visited.
                    if (steps.ContainsKey(neighbour))
                    {
                        continue;
                    }

                    steps[neighbour] = steps[current] + 1; // This cell is one step further away from the current cell.
                    frontier.Enqueue(neighbour); // Add it to the queue so that its neighbours can be checked later.
                    reachableCells.Add(neighbour); // Add it to the result since it's reachable within the movement range.
                }
            }

            return reachableCells;
        }
    }
}