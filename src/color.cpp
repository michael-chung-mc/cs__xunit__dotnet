#include "pch.h"

Color::Color(double red, double green, double blue)
{
	r = red;
	g = green;
	b = blue;
}

Color Color::operator+(Color x) {
	return Color(r + x.r, g + x.g, b + x.b);
}
Color Color::operator-(Color x) {
	return Color(r - x.r, g - x.g, b - x.b);
}
Color Color::operator*(float x) {
	return Color(r * x, g * x, b * x);
}
Color Color::operator*(Color x) {
	return Color(r * x.r, g * x.g, b * x.b);
}