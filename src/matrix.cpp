#include "pch.h"

Matrix::Matrix(int rows, int columns)
{
	rnum = rows;
	cnum = columns;
	this->grid = new double* [rnum];
	for (int i = 0; i < rnum; i++)
	{
		this->grid[i] = new double[cnum];
	}
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			this->grid[i][j] = 0;
		}
	}
}

Matrix::Matrix(Matrix& other)
{
	//std::cout << "matrix copy ctor" << std::endl;
	this->rnum = other.rnum;
	this->cnum = other.cnum;
	//std::cout << "this: " << rnum << cnum << " other:" << other.rnum << other.cnum << std::endl;
	this->grid = new double* [rnum];
	for (int i = 0; i < rnum; i++)
	{
		this->grid[i] = new double[cnum];
	}
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			this->grid[i][j] = other.grid[i][j];
		}
	}
}

Matrix::~Matrix()
{
	//std::cout << "deleting matrix" << &grid << std::endl;
	for (int i = 0; i < rnum; i++)
	{
		delete[] this->grid[i];
		this->grid[i] = nullptr;
	}
	delete[] this->grid;
	this->grid = nullptr;
}

Matrix& Matrix::operator=(Matrix other)
{
	//std::cout << "matrix copy assign" << std::endl;
	if (this == &other) return *this;
	this->rnum = other.rnum;
	this->cnum = other.cnum;
	for (int i = 0; i < rnum; i++)
	{
		delete[] this->grid[i];
		this->grid[i] = nullptr;
	}
	delete[] this->grid;
	this->grid = nullptr;
	this->grid = new double* [rnum];
	for (int i = 0; i < rnum; i++)
	{
		this->grid[i] = new double[cnum];
	}
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			this->grid[i][j] = 0;
		}
	}
	return *this;
}

bool Matrix::operator==(const Matrix other)
{
	//std::cout << "matrix equality" << std::endl;
	if (other.rnum != rnum || other.cnum != cnum)
	{
		return false;
	}
	for (int i = 0; i < rnum; i++)
	{
		for (int j = 0; j < cnum; j++)
		{
			if (other.grid[i][j] != this->grid[i][j])
			{
				return false;
			}
			//std::cout << &grid[i][j] << std::endl;
			//std::cout << &other.grid[i][j] << std::endl;
			//std::cout << "==" << std::endl;
		}
	}
	return true;
}

bool Matrix::checkValid(int row, int column)
{
	return row >= 0 && row < rnum && column >= 0 && column < cnum;
}

void Matrix::setRC(int row, int column, double value)
{
	if (checkValid(row, column))
	{
		this->grid[row][column] = value;
	}
}

double Matrix::getRC(int row, int column)
{
	if (checkValid(row, column))
	{
		return this->grid[row][column];
	}
}