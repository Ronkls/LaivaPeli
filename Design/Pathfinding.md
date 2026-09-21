# Grid Graph Pathfinding Design

Here is the design that I came up with for the Grid Graph cell point costs and how it could be implemented.

## Terrain Types

I think a good way to implement the types would be to make them an enum that has the type and the cost / value of the terrain.

Here are some terrain types and their cost values that I came up with.

```csharp
public enum TerrainType
{
    None = 0, // No terrain type has been assigned.
    CalmWater = 1, // Just your basic water. Has the lowest point cost.
    SlowCurrent = 2,
    RoughWater = 3,
    StrongCurrent = 4,
    Whirlpool = 10,
    Rocks = -1,
    WreckedShip = -1,
    Island = -1,
    Land = -1,
}
```

Positive cost terrain is navigable terrain.

Negative cost terrain is non-navigable terrain.

I don't know how necessary it is to have multiple terrain types with a negative value if they all do the same thing when it comes to pathfinding. But if there are some other things we want to check with the TerrainType that don't include their values, then having them as separate types would still be useful.

## Terrain Detection

One implementation for detecting terrain could be done using Area3D nodes and collision shapes.

When a cell is created, the NavigationGrid checks which Area3D is present at the cell's world position. The cell's cost is then changed based on the Area3D's or parent node's TerrainType value.

Another implementation could be done using a raycast from the cell's position. The raycast could be cast downwards and detect what terrain is located at that position. The object that the raycast hits could then have a TerrainType which would be used to determine the cost of the cell.

I think the raycast method could be useful for detecting the actual terrain directly underneath the cell, while Area3D could be useful for larger areas such as currents or whirlpools.

If some areas would overlap, the terrain that has the most restrictive terrain should take priority. If the overlapping area has a non-navigable terrain, then that should always take priority.

So there would be:

### Area3D

Represents a terrain region such as a current, water or an island.

### CollisionShape3D

Defines the boundary of a terrain Area3D. The shape should ideally match the actual shape and size of the terrain or area it represents.

### Raycast

Can be used from the cell's position to detect what terrain is directly underneath the cell. The object that is hit by the raycast can provide the TerrainType.

## Pseudocode

Here is what a pseudocode of my implementation would look like.

    When the NavigationGrid is ready:
        Build the grid

        For each cell in the grid:
            Determine the cell's world position

            Find all Area3D nodes that overlap the cell's position

            Use a raycast to check the terrain directly underneath the cell

            Create a list of all detected TerrainTypes

            If the list contains any non-navigable terrain:
                Set the cell's cost to -1

            Else:
                Find the TerrainType with the highest cost

                Set the cell's cost to that TerrainType's cost

            Store the cell in the grid


    To detect terrain using an Area3D:
        Check which Area3D nodes contain the cell's world position

        For each detected Area3D:
            Get its TerrainType

            Add the TerrainType to the list of detected terrain


    To detect terrain using a raycast:
        Start the raycast at the cell's world position

        Cast the ray downwards

        If the raycast hits an object:
            Check if the object has a TerrainType

            If it has a TerrainType:
                Add the TerrainType to the list of detected terrain


    To determine the final cost:
        If any detected TerrainType has a negative cost:
            Return -1

        Otherwise:
            Start with the default cost of 1

            For each detected TerrainType:
                If its cost is higher than the current cost:
                    Set the current cost to that TerrainType's cost

            Return the current cost