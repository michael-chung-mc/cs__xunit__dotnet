#pragma once
#ifndef COLOR_H
#define COLOR_H

#include "tuple.h"

class Color : public Tuple {
public:
	double r;
	double g;
	double b;
	Color();
	Color(double red, double green, double blue);
	Color operator+(Color x);
	Color operator-(Color x);
	Color operator*(float x);
	Color operator*(Color x);
	bool checkEqual(Color other);
};

#endif