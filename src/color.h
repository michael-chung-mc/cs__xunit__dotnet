#include "pch.h"

class Color : public Tuple {
public:
	double r;
	double g;
	double b;
	Color(double red, double green, double blue);
	Color operator+(Color x);
	Color operator-(Color x);
	Color operator*(float x);
	Color operator*(Color x);
};