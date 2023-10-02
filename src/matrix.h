#pragma once
#ifndef MATRIX_H
#define MATRIX_H

class Matrix {
public:
	double **grid;
	int rnum;
	int cnum;
	Matrix();
	Matrix(int row, int column);
	Matrix(const Matrix& other);
	Matrix(int row, int column, double* values);
	~Matrix();
	Matrix& operator=(const Matrix other);
	bool operator==(const Matrix other) const;
	Matrix* operator*(const Matrix other);
	Tuple operator*(const Tuple other);
	Point operator*(const Point other);
	Vector operator*(const Vector other);
	bool checkInvertible();
	bool checkValid(int row, int column);
	bool checkEqual(const Matrix other);
	double getRC(int row, int column);
	void setRC(int row, int column, double value);
	Matrix* transpose();
	double determinant();
	Matrix* submatrix(int row, int column);
	double minor(int row, int column);
	double cofactor(int row, int column);
	Matrix* invert();
	Matrix* identity();
	Matrix* rotateX(double radians);
	Matrix* rotateY(double radians);
	Matrix* rotateZ(double radians);
	Matrix* scale(int x, int y, int z);
	Matrix* translate(int x, int y, int z);
	Matrix* shear(double xy, double xz, double yx, double yz, double zx, double zy);
};

class IdentityMatrix : public Matrix {
public:
	IdentityMatrix(int rows, int columns);
};

class TranslationMatrix : public Matrix {
public:
	TranslationMatrix(double x, double y, double z);
};

class ScalingMatrix : public Matrix {
public:
	ScalingMatrix(double x, double y, double z);
};

class XRotationMatrix : public Matrix {
public:
	XRotationMatrix(double radians);
};

class YRotationMatrix : public Matrix {
public:
	YRotationMatrix(double radians);
};

class ZRotationMatrix : public Matrix {
public:
	ZRotationMatrix(double radians);
};

class ShearingMatrix : public Matrix {
public:
	ShearingMatrix(double xy, double xz, double yx, double yz, double zx, double zy);
};

#endif