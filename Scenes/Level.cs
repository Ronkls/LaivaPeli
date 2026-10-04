using GA.Ships.Pathfinding;
using Godot;
using GA.Common;
using System.Collections.Generic;
using Cell = GA.Ships.Pathfinding.NavigationGrid.Cell;

namespace GA.Ships
{
	public partial class Level : Node3D
	{
		#region Statics
		private static Level _current = null;

		public static Level Current { get { return _current; } }
		#endregion Statics

		[Export] private NavigationGrid _grid = null;

		public Pathfinder Pathfinder
		{
			get;
			private set;
		}

		public Level()
		{
			_current = this;
		}

		public override void _Ready()
		{
			if (_grid == null)
			{
				_grid = this.GetNode<NavigationGrid>(recursive: false);
			}

			Pathfinder = new Pathfinder(_grid);

            //Ignore, just wanted to test if it worked and this was the easiest way without writing unit tests.
            Cell start = _grid.GetCell(new Vector3(-9, 0, 25)); // If the position was -50, 0, 50 the starting cell would be non-walkable and it throws the corret ArgumentException

            IList<Cell> reachableCells = Pathfinder.GetReachableCells(start, 1);
            GD.Print($"{reachableCells.Count} <-- this value should be 4");

            reachableCells = Pathfinder.GetReachableCells(start, 2);
            GD.Print($"{reachableCells.Count} <-- this value should be 12");

            reachableCells = Pathfinder.GetReachableCells(start, 0);
            GD.Print($"{reachableCells.Count} <-- this value should be 0");

            reachableCells = Pathfinder.GetReachableCells(start, 4);
            GD.Print($"{reachableCells.Count} <-- this value should be something because it hits an obstacle");
		}
	}
}