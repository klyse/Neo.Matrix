# Neo.Matrix

This is a two dimensional matrix helper for C#.

[![Nuget](https://img.shields.io/nuget/v/neo.matrix?style=plastic)](https://www.nuget.org/packages/Neo.Matrix/)

[![Build status](https://dev.azure.com/derKlyse/GitBuilds/_apis/build/status/Neo.Matrix?branchName=master)](https://dev.azure.com/derKlyse/GitBuilds/_build/latest?definitionId=6)

# Install
``Install-Package Neo.Matrix -Version 2.0.0``

# Usage

Function parameter order: Rows, Columns

## Creat Matrix

New Matrix 6x5:
```csharp
var matrix = new Matrix<int>(rows: 5, columns: 5);
```

New Matrix 2x3 filled up with 4:
```csharp
var newMatrix = Matrix<int>.NewMatrix(rows: 2, columns: 3, filler: 4);
```
New Matrix with custom predefined content:
```csharp
var expected = new Matrix<int?>(new int?[,]
{
    {null, 1},
    {2, null}
});
```

Matrix with a custom object:
```csharp
class DummyObject
{
    public int Value { get; set; }
}

var matrix = Matrix<DummyObject>.NewMatrix(4, 4);
```

New Matrix 4x4 with custom filler function:
```csharp

var val = 0;
var matrix = Matrix<DummyObject>.NewMatrix(4, 4, () =>
{
    val++;
    return new DummyObject
    {
        Value = val
    };
});
```

Output:
|  |  |  |  |
|-|-|-|-|
| 1 | 2 | 3 | 4 |
| 5 | 6 | 7 | 8 |
| 9 | 10 | 11 | 12 |
| 13 | 14 | 15 | 16 |

## Equality

Equality check is computed on the content of the matrix by calling the ``GetHashCode()`` function on all elements.

Check equality of matrix:

```csharp
var newMatrix = Matrix<int>.NewMatrix(rows: 2, columns: 3, filler: 4);

var expected = new Matrix<int>(new[,]
{
    {4, 4, 4},
    {4, 4, 4}
});

var eq = newMatrix.Equals(expected); // true
```

## Access Data

Get a data of data:
```csharp
var field = matrix[1,1]; // get field at 1/1

var field = matrix.GetAbove(1,1); // gets value from 0, 1
var field = matrix.GetBelow(1,1); // gets value from 2, 1
var field = matrix.GetLeft(1,1); // gets value from 1, 0
var field = matrix.GetRight(1,1); // gets value from 1, 2
```

Get a row/column of data:
```csharp
var col = matrix.GetCol(1); // get column 1
var row = matrix.GetRow(1); // get row 1
```

Get a flat (unidimensional) representation of data:

```csharp
var flat = matrix.GetFlat();
```

## Access a subset of data

Requests can be chained:
```csharp
var mat5x5 = Matrix<int>.NewMatrix(10, 10, 0) // creates a new matrix 10x10
                        .GetRect(5, 5, 5, 5); // selects a 5x5 matrix from the center
```

Add some padding around the matrix:
```csharp
var mat15x15 = Matrix<int>.NewMatrix(10, 10, 0)      // creates a new matrix 10x10
                          .GetRect(5, 5, 5, 5)       // selects a 5x5 matrix from the center
                          .AddPadding(5, filler: 3); // add a top, right, bottom, left 5 elements padding and fillup with 3; new size is 15x15
```