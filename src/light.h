#pragma once
#ifndef Light_H
#define Light_H

#include "tuple.h"
#include "color.h"

class PointSource {
public:
    Point position;
    Color intensity;
    PointSource(Point position, Color intensity);
};

#endif