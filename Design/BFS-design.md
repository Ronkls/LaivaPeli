# BFS Design

## Diagonal Pathfinding

With the current BFS it only supports axial movement, meaning a cell can only move up, down, left, or right but not diagonally.
If diagonal movement was enabled then each cell could have up to 8 neighbours instead of 4. This means there would be more reachable cells with the same number of steps.

One problem might be that diagonal movement could allow the path to move between two non-walkable cells. The path could move diagonally between the two blocked cells, which may not make sense depending on how the obstacles are placed in the level.

Diagonal movement would also raise the question of whether diagonal moves should have the same cost as normal moves. Since we are moving a greater distance when moving diagonally i would say that diagonal diagonal movement should cost more.

Cheated a bit and looked at if diagonal movement is costs more in A* and it does. It cost more because something something Pythagorean Theorem.
So if straight movement cost is like 10 the diagonal would be 10 * 1.414 so the diagonal cost would be 14.