#pragma once
#ifndef PATTERN_H
#define PATTERN_H

#include "color.h"

class Pattern {
public:
    Color argBlack;
    Color argWhite;
    Pattern();
};

class PatternStripe : public Pattern {
public:
    PatternStripe();
};

#endif