#pragma once
#ifndef Ray_H
#define Ray_H
class Ray {
public:
	Point origin;
	Vector direction;
	Ray(Point origin, Vector direction);
	Point position(double time);
	Ray transform(TranslationMatrix matrix);
	Ray transform(ScalingMatrix matrix);
};
#endif