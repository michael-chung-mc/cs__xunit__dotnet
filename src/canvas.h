#pragma once
#ifndef Canvas_H
#define Canvas_H

class Canvas {
public:
	int w;
	int h;
	std::vector<std::vector<Color>> grid;
	Canvas(int width, int height);
	Color getPixel(int x, int y);
	bool isClean();
	bool inBounds(int x, int y);
	void setPixel(int x, int y, Color c);
};

#endif