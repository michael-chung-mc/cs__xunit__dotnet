#include "pch.h"
#include "camera.h"
#include "tuple.h"
#include "canvas.h"
#include "color.h"
#include "light.h"
#include "ray.h"
#include "material.h"
#include "intersection.h"
#include "sphere.h"

class Projectile {
public:
	Projectile(Point pos, Vector vel)
	{
		position = pos;
		velocity = vel;
	}
	Point position;
	Vector velocity;
};

class Environment {
public:
	Environment(Vector g, Vector w)
	{
		gravity = g;
		wind = w;
	}
	Vector gravity;
	Vector wind;
};

class Simulation {
private:
	Canvas c = Canvas(getPPMWidth(), getPPMHeight());
public:
	Projectile tick(Environment env, Projectile ball)
	{
		Point position = ball.position + ball.velocity;
		Vector velocity = ball.velocity + env.gravity + env.wind;
		return Projectile(position, velocity);
	}
	void fire(int power) {
		c.fill(Color(1, 1, 1));
		//Projectile b = Projectile(Point(0, 1, 0), (Vector(1, 1, 0)*power).normalize());
		Projectile b = Projectile(Point(0, 1, 0), (Vector(1, 1, 0).normalize()) * power);
		Environment e = Environment(Vector(0, -.1, 0), Vector(-.01, 0, 0));
		while (b.position.y >= 0)
		{
			b = tick(e,b);
			std::cout << "x: " << std::to_string(b.position.x) << " y: " << std::to_string(b.position.y) << " z: " << std::to_string(b.position.z) << " w: " << std::to_string(b.position.w) << std::endl;
			Color white = Color(0, 0, 0);
			c.setPixel((int)b.position.x, getPPMHeight() - b.position.y, white);
		}
		c.save();
	}
};

void shadowTracer()
{
	Point pov = Point(0,0,-5);
	double wallWidth = 10;
	double wallZ = 10;
	double wallXY = wallWidth/2;
	double pixels = 100;
	double pixelSize = wallWidth/pixels;
	Canvas canvas = Canvas(pixels,pixels);
	Color hitColor = Color(1,0,0);

	Sphere obj = Sphere();

	for (int i = 0; i <= pixels; i++)
	{
		double worldY = wallXY - pixelSize * i;
		for (int j = 0; j <= pixels; j++)
		{
			double worldX = -wallXY + pixelSize * j;
			Point position = Point(worldX, worldY, wallZ);
			//std::cout << worldX << worldY << wallZ << std::endl;
			Ray r = Ray(pov, (position-pov).normalize());
			std::vector<Intersection> xs = obj.intersect(r);
			if (xs.size() != 0)
			{
				canvas.setPixel(i,j,hitColor);
			}
		}
	}
	canvas.save();
}

void shadingTracer(Point argPov, PointSource argLight, double argScreenWidth, double argScreenZ, double argScreenPixels, Color argSphereColor)
{
	Canvas varCanvas = Canvas(argScreenPixels,argScreenPixels);
	double varScreenXY = argScreenWidth/2;
	double varPixelSize = argScreenWidth/argScreenPixels;

	Sphere varObj = Sphere();
	varObj.material = Material();
	//varObj.material.shininess = 0;
	varObj.material.color = argSphereColor;

	PointSource varLight = argLight;

	for (int i = 0; i <= argScreenPixels; i++)
	{
		double varWorldY = varScreenXY - varPixelSize * i;
		for (int j = 0; j <= argScreenPixels; j++)
		{
			double varWorldX = -varScreenXY + varPixelSize * j;
			Point varWorldPosition = Point(varWorldX, varWorldY, argScreenZ);
			//std::cout << worldX << worldY << wallZ << std::endl;
			Ray r = Ray(argPov, (varWorldPosition-argPov).normalize());
			std::vector<Intersection> xs = varObj.intersect(r);
			if (xs.size() != 0)
			{
				Point p = r.position(xs[0].time);
				Vector normal = xs[0].object.normal(p);
				Vector pov = -r.direction;
				Color shade = varObj.material.getLighting(varLight, p, pov, normal);
				varCanvas.setPixel(i,j,shade);
			}
		}
	}
	varCanvas.save();
}

void cameraRender()
{
	PointSource varLight = PointSource(Point(-10,10,-10), Color(1,1,1));

	Sphere varFloor = Sphere();
	varFloor.transform = ScalingMatrix(10,0.01,10);
	varFloor.material = Material();
	varFloor.material.color = Color(1,0.9,0.9);
	varFloor.material.specular = 0;

	Sphere varLeftWall = Sphere();
	varLeftWall.transform = *(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(-getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,0.01,10));
	varLeftWall.material = varFloor.material;

	Sphere varRightWall = Sphere();
	varRightWall.transform = *(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,0.01,10));
	varRightWall.material = varFloor.material;

	Sphere varObjMid = Sphere();
	varObjMid.transform = TranslationMatrix(-0.5,1,0.5);
	varObjMid.material = Material();
	varObjMid.material.color = Color(0.1,1,0.5);
	varObjMid.material.diffuse = 0.7;
	varObjMid.material.specular = 0.3;

	Sphere varObjRight = Sphere();
	varObjRight.transform = *(TranslationMatrix(1.5,0.5,-0.5) * ScalingMatrix(0.5,0.5,0.5)); 
	varObjRight.material = Material();
	varObjRight.material.color = Color(0.5,1,0.1);
	varObjRight.material.diffuse = 0.7;
	varObjRight.material.specular = 0.3;

	Sphere varObjLeft = Sphere();
	varObjLeft.transform = *(TranslationMatrix(-1.5,0.33,-0.75) * ScalingMatrix(0.33,0.33,0.33)); 
	varObjLeft.material = Material();
	varObjLeft.material.color = Color(1,0.8,0.1);
	varObjLeft.material.diffuse = 0.7;
	varObjLeft.material.specular = 0.3;

	World varEnv = World();
	varEnv.objects.push_back(varFloor);
	varEnv.objects.push_back(varLeftWall);
	varEnv.objects.push_back(varRightWall);
	varEnv.objects.push_back(varObjMid);
	varEnv.objects.push_back(varObjRight);
	varEnv.objects.push_back(varObjLeft);
	varEnv.lights.push_back(varLight);

	Camera varCamera = Camera(100,50,getPI()/3);
	varCamera.mbrTransform = ViewMatrix(Point(0,1.5,-5), Point(0,1,0), Vector(0,1,0));

	Canvas img = varCamera.render(varEnv);
	img.save();
}

int main(int argc, char **argv)
{
    // ::testing::InitGoogleTest( &argc, argv);
    // return RUN_ALL_TESTS();

	// double arr[] = { 2, 0, 0, 0, 0, 1, 0, 0, 0,0,1,0 ,0 ,0 ,0, 1 };
	// Matrix m = Matrix(4, 4, arr);
	// Tuple t = Tuple(2, 2, 2, 2);
	// Tuple x = m * t;

	// int firepower = 10;
	// std::cout << "Firing: " << firepower << std::endl;
	// Simulation sim;
	// sim.fire(firepower);

	// shadowTracer();

// 	shadingTracer(Point(0,0,-5), PointSource(Point(-10,10,-10),Color(1,1,1)), 10, 10, 100, Color(1, 0.2, 1));
// 	//shadingTracer(Point(0,0,-5), PointSource(Point(-10,10,-10),Color(0.5,0.5,0.5)), 10, 10, 100, Color(1, 0.2, 1));

	cameraRender();

	return 0;
}