#include "light.h"
#include "tuple.h"
#include "color.h"

PointSource::PointSource()
{
    this->position = Point(0,0,0);
    this->intensity = Color(0,0,0);
}

PointSource::PointSource(Point position, Color intensity)
{
    this->position = position;
    this->intensity = intensity;
}