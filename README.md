# Martian Robots

## Problem Overview
This project solves the Martian Robots challenge, where robots navigate a rectangular grid on Mars, following instructions and avoiding falling off the edge. Robots leave a "scent" if lost, preventing future robots from being lost at the same spot and orientation.

## Features
- Supports extensible robot commands (L, R, F)
- Handles lost robots and scent logic
- Unit tested with xUnit
- .NET 9 compatible

## Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) installed

## Setup
1. Clone the repository or download the source code.
2. Ensure you are in the project root directory.

## Build
```
dotnet build
```

## Run
The program reads input from `input.txt` in the project root. To run:
```
dotnet run --project MartianRobots
```

## Test
Unit tests are written with xUnit. To run tests:
```
dotnet test
```

## Input Format
- The first line: upper-right coordinates of the grid (e.g., `5 3`)
- Each robot: two lines
  - Line 1: initial position and orientation (e.g., `1 1 E`)
  - Line 2: instruction string (e.g., `RFRFRFRF`)

## Output Format
- For each robot, prints final position and orientation, appending `LOST` if the robot fell off the grid.

## Example
**input.txt:**
```
5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFLFLFL
```

**Output:**
```
1 1 E
3 3 N LOST
2 3 S
```

## Notes
- The solution is designed for extensibility (e.g., adding new commands).
- If you encounter package restore issues, ensure your NuGet sources are set up and you are using .NET 9 SDK. 