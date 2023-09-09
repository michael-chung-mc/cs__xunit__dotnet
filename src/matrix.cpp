#include "pch.h"

Matrix::Matrix(int rows, int columns)
{
	rnum = rows;
	cnum = columns;
	grid = new double* [rnum];
	for (int i = 0; i < rnum; i++)
	{
		grid[i] = new double[cnum];
	}
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			grid[i][j] = 0;
		}
	}
}

Matrix::~Matrix()
{
	for (int i = 0; i < rnum; i++)
	{
		delete grid[i];
	}
	delete grid;
	grid = nullptr;
}

bool Matrix::checkValid(int row, int column)
{
	return row >= 0 && row < rnum && column >= 0 && column < cnum;
}

void Matrix::setRC(int row, int column, double value)
{
	if (checkValid(row, column))
	{
		grid[row][column] = value;
	}
}

double Matrix::getRC(int row, int column)
{
	if (checkValid(row, column))
	{
		return grid[row][column];
	}
}