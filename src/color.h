#pragma once
#ifndef COLOR_H
#define COLOR_H

#include "tuple.h"

class Color : public Tuple {
public:
	double mbrRed;
	double mbrGreen;
	double mbrBlue;
	Color();
	Color(double red, double green, double blue);
	Color operator+(Color x);
	Color operator-(Color x);
	Color operator*(float x);
	Color operator*(Color x);
	bool checkEqual(const Color other) const;
};

#endif