#pragma once
#ifndef TUPLE_H
#define TUPLE_H

class Tuple {
public:
	double mbrX;
	double mbrY;
	double mbrZ;
	double mbrW;
	Tuple();
	Tuple(double argx, double argy, double argz, double argw);
	Tuple operator-();
	Tuple operator*(float multiple);
	Tuple operator/(float multiple);
	Tuple operator+(Tuple a);
	virtual Tuple add(Tuple a);
	Tuple subtract(Tuple a);
	virtual Tuple negate();
	virtual float magnitude();
	double dot(Tuple a);
	Tuple reflect(Tuple normal);
};

class Vector : public Tuple {
public:
	Vector();
	Vector(double argx, double argy, double argz);
	Vector operator-();
	Vector operator+(Tuple a);
	Vector operator*(float multiple);
	Vector subtract(Tuple a);
	Vector normalize();
	double dot(Vector a);
	Vector cross(Vector a);
	Vector reflect(Vector normal);
};

class Point : public Tuple {
public:
	Point();
	Point(double argx, double argy, double argz);
	Point operator+(Tuple a);
	Vector operator-(Point a);
	Point subtract(Tuple a);
};

#endif