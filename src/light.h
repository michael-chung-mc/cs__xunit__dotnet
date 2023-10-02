#pragma once
#ifndef LIGHT_H
#define LIGHT_H

#include "tuple.h"
#include "color.h"

class PointSource {
public:
    Point position;
    Color intensity;
    PointSource();
    PointSource(Point position, Color intensity);
    bool checkEqual(const PointSource &other) const;
};

#endif