#include "color.h"
#include "comparinator.h"
#include "tuple.h"

Color::Color()
{
	mbrRed = 0;
	mbrGreen = 0;
	mbrBlue = 0;
}
Color::Color(double red, double green, double blue)
{
	mbrRed = red;
	mbrGreen = green;
	mbrBlue = blue;
}

Color Color::operator+(Color x) {
	return Color(mbrRed + x.mbrRed, mbrGreen + x.mbrGreen, mbrBlue + x.mbrBlue);
}
Color Color::operator-(Color x) {
	return Color(mbrRed - x.mbrRed, mbrGreen - x.mbrGreen, mbrBlue - x.mbrBlue);
}
Color Color::operator*(float x) {
	return Color(mbrRed * x, mbrGreen * x, mbrBlue * x);
}
Color Color::operator*(Color x) {
	return Color(mbrRed * x.mbrRed, mbrGreen * x.mbrGreen, mbrBlue * x.mbrBlue);
}
bool Color::checkEqual(const Color other) const
{
	Comparinator ce = Comparinator();
	return ce.checkFloat(mbrRed,other.mbrRed) && ce.checkFloat(mbrGreen,other.mbrGreen) && ce.checkFloat(mbrBlue,other.mbrBlue);
}