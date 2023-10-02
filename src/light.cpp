#include "light.h"
#include "tuple.h"
#include "color.h"
#include "comparinator.h"

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

bool PointSource::checkEqual(const PointSource &other) const
{
    Comparinator ce = Comparinator();
    return ce.checkTuple(this->position,other.position) && intensity.checkEqual(other.intensity);
}