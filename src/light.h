#include "pch.h"

#pragma once
#ifndef Light_H
#define Light_H

class PointSource {
public:
    Point position;
    Color intensity;
    PointSource(Point position, Color intensity);
};

#endif