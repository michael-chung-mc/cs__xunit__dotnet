#include "color.h"
#include "comparinator.h"
#include "tuple.h"

Color::Color()
{
	r = 0;
	g = 0;
	b = 0;
}
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
bool Color::checkEqual(const Color other) const
{
	Comparinator ce = Comparinator();
	return ce.checkFloat(r,other.r) && ce.checkFloat(g,other.g) && ce.checkFloat(b,other.b);
}