#include "pch.h"
#include "camera.h"
#include "tuple.h"
#include "canvas.h"
#include "color.h"
#include "light.h"
#include "ray.h"
#include "material.h"
#include "intersection.h"
#include "form.h"
#include "pattern.h"

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
		while (b.position.mbrY >= 0)
		{
			b = tick(e,b);
			std::cout << "x: " << std::to_string(b.position.mbrX) << " y: " << std::to_string(b.position.mbrY) << " z: " << std::to_string(b.position.mbrZ) << " w: " << std::to_string(b.position.mbrW) << std::endl;
			Color white = Color(0, 0, 0);
			c.setPixel((int)b.position.mbrX, getPPMHeight() - b.position.mbrY, white);
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
			// std::vector<Intersection> xs = obj.getIntersections(r).mbrIntersections;
			std::vector<std::unique_ptr<Intersection>> xs = obj.getIntersections(r).mbrIntersections;
			if (xs.size() != 0)
			{
				canvas.setPixel(i,j,hitColor);
			}
		}
	}
	canvas.save();
}

void shadingTracerSphere(Point argPov, PointSource argLight, double argScreenWidth, double argScreenZ, double argScreenPixels, Color argSphereColor)
{
	Canvas varCanvas = Canvas(argScreenPixels,argScreenPixels);
	double varScreenXY = argScreenWidth/2;
	double varPixelSize = argScreenWidth/argScreenPixels;

	Sphere varObj = Sphere();
	varObj.setMaterial(Material());
	//varObj.material.shininess = 0;
	varObj.mbrMaterial->mbrColor = argSphereColor;

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
			// std::vector<Intersection> xs = varObj.getIntersections(r).mbrIntersections;
			std::vector<std::unique_ptr<Intersection>> xs = varObj.getIntersections(r).mbrIntersections;
			if (xs.size() != 0)
			{
				// Point p = r.getPosition(xs[0].mbrTime);
				// Vector normal = xs[0].mbrObject->getNormal(p);
				Point p = r.getPosition(xs[0]->mbrTime);
				Vector normal = xs[0]->mbrObject->getNormal(p);
				Vector pov = -r.mbrDirection;
				Color shade = varObj.mbrMaterial->getColor(varLight, p, pov, normal, false);
				varCanvas.setPixel(i,j,shade);
			}
		}
	}
	varCanvas.save();
}

void cameraRenderSpheres()
{
	PointSource varLight = PointSource(Point(-10,10,-10), Color(1,1,1));

	Sphere varFloor = Sphere();
	varFloor.setTransform(ScalingMatrix(10,0.01,10));
	varFloor.setMaterial(Material());
	varFloor.mbrMaterial->mbrColor = Color(1,0.9,0.9);
	varFloor.mbrMaterial->mbrSpecular = 0;

	Sphere varLeftWall = Sphere();
	varLeftWall.setTransform(*(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(-getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,0.01,10)));
	varLeftWall.setMaterial(*varFloor.mbrMaterial);

	Sphere varRightWall = Sphere();
	varRightWall.setTransform(*(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,0.01,10)));
	varRightWall.setMaterial(*varFloor.mbrMaterial);

	Sphere varObjMid = Sphere();
	varObjMid.setTransform(TranslationMatrix(-0.5,1,0.5));
	varObjMid.setMaterial(Material());
	varObjMid.mbrMaterial->mbrColor = Color(0.1,1,0.5);
	varObjMid.mbrMaterial->mbrDiffuse = 0.7;
	varObjMid.mbrMaterial->mbrSpecular = 0.3;

	Sphere varObjRight = Sphere();
	varObjRight.setTransform(*(TranslationMatrix(1.5,0.5,-0.5) * ScalingMatrix(0.5,0.5,0.5))); 
	varObjRight.setMaterial(Material());
	varObjRight.mbrMaterial->mbrColor = Color(0.5,1,0.1);
	varObjRight.mbrMaterial->mbrDiffuse = 0.7;
	varObjRight.mbrMaterial->mbrSpecular = 0.3;

	Sphere varObjLeft = Sphere();
	varObjLeft.setTransform(*(TranslationMatrix(-1.5,0.33,-0.75) * ScalingMatrix(0.33,0.33,0.33))); 
	varObjLeft.setMaterial(Material());
	varObjLeft.mbrMaterial->mbrColor = Color(1,0.8,0.1);
	varObjLeft.mbrMaterial->mbrDiffuse = 0.7;
	varObjLeft.mbrMaterial->mbrSpecular = 0.3;

	World varEnv = World();
	varEnv.setObject(new Sphere(varFloor));
	varEnv.setObject(new Sphere(varLeftWall));
	varEnv.setObject(new Sphere(varRightWall));
	varEnv.setObject(new Sphere(varObjMid));
	varEnv.setObject(new Sphere(varObjRight));
	varEnv.setObject(new Sphere(varObjLeft));
	// varEnv.setObject(std::make_unique<Sphere>(varFloor));
	// varEnv.setObject(std::make_unique<Sphere>(varLeftWall));
	// varEnv.setObject(std::make_unique<Sphere>(varRightWall));
	// varEnv.setObject(std::make_unique<Sphere>(varObjMid));
	// varEnv.setObject(std::make_unique<Sphere>(varObjRight));
	// varEnv.setObject(std::make_unique<Sphere>(varObjLeft));
	varEnv.mbrLights.push_back(varLight);

	Camera varCamera = Camera(100,50,getPI()/3);
	varCamera.setTransform(new ViewMatrix(Point(0,1.5,-5), Point(0,1,0), Vector(0,1,0)));

	Canvas img = varCamera.render(varEnv);
	img.save();
}

void cameraRenderPlane()
{
	PointSource varLight = PointSource(Point(-10,10,-10), Color(1,1,1));

	Plane varWall = Plane();
	// varWall.setTransform(TranslationMatrix(0,0,5));
	// varWall.setTransform(XRotationMatrix(getPI()/4));
	varWall.setTransform(IdentityMatrix(4,4).rotateX(getPI()/2));
	// varWall.setTransform(*(TranslationMatrix(0,0,5).rotateY(-getPI()/4)->rotateX(getPI()/2)->scale(10,0.01,10)));
	// varWall.setTransform(*(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(-getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,0.01,10)));
	varWall.setMaterial(Material());
	// varWall.mbrMaterial->setPattern(new PatternChecker3d(Color(0,0,0), Color(1,1,1)));
	varWall.mbrMaterial->setPattern(new PatternRing(Color(1,0,0), Color(1,1,1)));
	// varWall.mbrMaterial->mbrPattern->setTransform(ScalingMatrix(2,2,2));
	// varWall.mbrMaterial->mbrPattern->setTransform(ScalingMatrix(0.05,0.05,0.05));

	World varEnv = World();
	varEnv.setObject(new Plane(varWall));
	// varEnv.setObject(std::make_unique<Plane>(varWall));
	varEnv.mbrLights.push_back(varLight);

	Camera varCamera = Camera(160,90,getPI()/2);
	varCamera.setTransform(new ViewMatrix(Point(0,5,-10), Point(0,5,0), Vector(0,1,0)));

	Canvas img = varCamera.render(varEnv);
	img.save();
}
void cameraRenderPlanes()
{
	PointSource varLight = PointSource(Point(-10,10,-10), Color(1,1,1));

	Plane varFloor = Plane();
	varFloor.setTransform(ScalingMatrix(10,0.01,10));
	varFloor.setMaterial(Material());
	varFloor.mbrMaterial->mbrColor = Color(1,0.9,0.9);
	varFloor.mbrMaterial->mbrSpecular = 0;

	Plane varLeftWall = Plane();
	varLeftWall.setTransform(*(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(-getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,0.01,10)));
	varLeftWall.setMaterial(*varFloor.mbrMaterial);

	Plane varRightWall = Plane();
	varRightWall.setTransform(*(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,0.01,10)));
	varRightWall.setMaterial(*varFloor.mbrMaterial);

	World varEnv = World();
	varEnv.setObject(new Plane(varFloor));
	varEnv.setObject(new Plane(varLeftWall));
	varEnv.setObject(new Plane(varRightWall));
	// varEnv.setObject(std::make_unique<Plane>(varFloor));
	// varEnv.setObject(std::make_unique<Plane>(varLeftWall));
	// varEnv.setObject(std::make_unique<Plane>(varRightWall));
	varEnv.mbrLights.push_back(varLight);

	Camera varCamera = Camera(100,50,getPI()/3);
	varCamera.setTransform(new ViewMatrix(Point(0,1.5,-5), Point(0,1,0), Vector(0,1,0)));

	Canvas img = varCamera.render(varEnv);
	img.save();
}

void cameraRenderSpherePlane()
{
	PointSource varLight = PointSource(Point(-10,10,-10), Color(1,1,1));

	Plane varFloor = Plane();
	varFloor.setTransform(*(TranslationMatrix(0,-1,0) * ScalingMatrix(10,1,10)));
	varFloor.setMaterial(Material());
	varFloor.mbrMaterial->mbrSpecular = 1;
	varFloor.mbrMaterial->setPattern(new PatternChecker3d(Color(0,0,0), Color(1,1,1)));
	varFloor.mbrMaterial->mbrPattern->setTransform(ScalingMatrix(1,0.1,1));

	Plane varLeftWall = Plane();
	varLeftWall.setTransform(*(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(-getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,1,10)));
	varLeftWall.setMaterial(Material());
	varLeftWall.mbrMaterial->setPattern(new PatternGradient(Color(1,0,0), Color(0,0,1)));
	varLeftWall.mbrMaterial->mbrPattern->setTransform(ScalingMatrix(1,1,2));

	Plane varRightWall = Plane();
	varRightWall.setTransform(*(*(*(TranslationMatrix(0,0,5) * YRotationMatrix(getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(10,1,10)));
	varRightWall.setMaterial(Material());
	varRightWall.mbrMaterial->setPattern(new PatternRing(Color(1,0,0), Color(1,1,1)));
	varRightWall.mbrMaterial->mbrPattern->setTransform(ScalingMatrix(0.05,0.05,0.05));

	Sphere varObjMid = Sphere();
	varObjMid.setTransform(TranslationMatrix(-0.5,1,0.5));
	varObjMid.setMaterial(Material());
	varObjMid.mbrMaterial->mbrColor = Color(0.1,1,0.5);
	varObjMid.mbrMaterial->mbrDiffuse = 0.7;
	varObjMid.mbrMaterial->mbrSpecular = 0.3;

	Sphere varObjRight = Sphere();
	varObjRight.setTransform(*(TranslationMatrix(1.5,0.5,-0.5) * ScalingMatrix(2,0.5,2))); 
	varObjRight.setMaterial(Material());
	varObjRight.mbrMaterial->mbrDiffuse = 0.7;
	varObjRight.mbrMaterial->mbrSpecular = 0.3;
	varObjRight.mbrMaterial->setPattern(new PatternStripe(Color(1,0,0), Color(0,0,1)));
	varObjRight.mbrMaterial->mbrPattern->setTransform(ScalingMatrix(0.25,0.25,0.25));

	Sphere varObjLeft = Sphere();
	varObjLeft.setTransform(*(TranslationMatrix(-1.5,0.33,-0.75) * ScalingMatrix(0.33,0.33,0.33))); 
	varObjLeft.setMaterial(Material());
	varObjLeft.mbrMaterial->mbrColor = Color(1,0.8,0.1);
	varObjLeft.mbrMaterial->mbrDiffuse = 0.7;
	varObjLeft.mbrMaterial->mbrSpecular = 0.3;

	World varEnv = World();
	varEnv.setObject(new Plane(varFloor));
	varEnv.setObject(new Plane(varLeftWall));
	varEnv.setObject(new Plane(varRightWall));
	varEnv.setObject(new Sphere(varObjMid));
	varEnv.setObject(new Sphere(varObjRight));
	varEnv.setObject(new Sphere(varObjLeft));
	// varEnv.setObject(std::make_unique<Sphere>(varFloor));
	// varEnv.setObject(std::make_unique<Sphere>(varLeftWall));
	// varEnv.setObject(std::make_unique<Sphere>(varRightWall));
	// varEnv.setObject(std::make_unique<Sphere>(varObjMid));
	// varEnv.setObject(std::make_unique<Sphere>(varObjRight));
	// varEnv.setObject(std::make_unique<Sphere>(varObjLeft));
	varEnv.mbrLights.push_back(varLight);

	Camera varCamera = Camera(100,50,getPI()/2);
	varCamera.setTransform(new ViewMatrix(Point(0,1.5,-5), Point(0,1,0), Vector(0,1,0)));

	Canvas img = varCamera.render(varEnv);
	img.save();
}

void cameraRenderPatterns()
{
	PointSource varLightL = PointSource(Point(-10,10,-10), Color(1,1,1));
	PointSource varLightR = PointSource(Point(10,10,-10), Color(1,1,1));

	Plane varFloor = Plane();
	varFloor.setTransform(TranslationMatrix(0,-1,0));
	// varFloor.setTransform(*(TranslationMatrix(0,-1,0) * ScalingMatrix(1,1,1)));
	varFloor.setMaterial(Material());
	varFloor.mbrMaterial->setPattern(new PatternChecker(Color(0,0,0), Color(1,1,1)));
	// varFloor.mbrMaterial->mbrPattern->setTransform(ScalingMatrix(10,1,10));

	Plane varLeftWall = Plane();
	varLeftWall.setTransform(*IdentityMatrix(4,4).translate(0,0,5)->rotateY(-getPI()/4)->rotateX(getPI()/2));
	// varLeftWall.setTransform(*(*(*(TranslationMatrix(0,10,5) * YRotationMatrix(-getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(0.1,1,0.1)));
	varLeftWall.setMaterial(Material());
	varLeftWall.mbrMaterial->setPattern(new PatternGradient(Color(1,0,0), Color(0,0,1)));
	varLeftWall.mbrMaterial->mbrPattern->setTransform(ScalingMatrix(2,2,2));

	Plane varRightWall = Plane();
	varRightWall.setTransform(*IdentityMatrix(4,4).translate(0,0,5)->rotateY(getPI()/4)->rotateX(getPI()/2));
	// varRightWall.setTransform(*(*(*(TranslationMatrix(0,10,5) * YRotationMatrix(getPI()/4)) * XRotationMatrix(getPI()/2)) * ScalingMatrix(0.1,1,0.1)));
	varRightWall.setMaterial(Material());
	varRightWall.mbrMaterial->setPattern(new PatternRing(Color(1,0,0), Color(1,1,1)));
	// varRightWall.mbrMaterial->mbrPattern->setTransform(ScalingMatrix(10,1,10));

	Sphere varObjMid = Sphere();
	varObjMid.setTransform(IdentityMatrix(4,4).translate(0,2,-6)->scale(2,2,2));
	// varObjMid.setTransform(TranslationMatrix(-0.5,0.6,0.5).scale(2,0.5,2));
	varObjMid.setMaterial(Material());
	varObjMid.mbrMaterial->mbrColor = Color(0.1,1,0.5);
	varObjMid.mbrMaterial->mbrDiffuse = 0.7;
	varObjMid.mbrMaterial->mbrSpecular = 0.3;
	varObjMid.mbrMaterial->setPattern(new PatternGradient(Color(1,0,0), Color(0,0,1)));
	varObjMid.mbrMaterial->mbrPattern->setTransform(ScalingMatrix(2,2,2));

	Sphere varObjRight = Sphere();
	varObjRight.setTransform(IdentityMatrix(4,4).translate(6,2,-6)->scale(2,2,2));
	// varObjRight.setTransform(*(TranslationMatrix(1.5,0.5,-0.5) * ScalingMatrix(2,0.5,2))); 
	varObjRight.setMaterial(Material());
	varObjRight.mbrMaterial->mbrDiffuse = 0.7;
	varObjRight.mbrMaterial->mbrSpecular = 0.3;
	varObjRight.mbrMaterial->setPattern(new PatternStripe(Color(1,0,0), Color(0,0,1)));
	varObjRight.mbrMaterial->mbrPattern->setTransform(ScalingMatrix(0.25,0.25,0.25));

	Sphere varObjLeft = Sphere();
	varObjLeft.setTransform(IdentityMatrix(4,4).translate(-6,2,-6)->scale(2,2,2));
	// varObjLeft.setTransform(*(TranslationMatrix(-1.5,0.33,-2) * ScalingMatrix(0.33,0.33,0.33))); 
	varObjLeft.setMaterial(Material());
	varObjLeft.mbrMaterial->mbrColor = Color(1,0.8,0.1);
	varObjLeft.mbrMaterial->mbrDiffuse = 0.7;
	varObjLeft.mbrMaterial->mbrSpecular = 0.3;

	World varEnv = World();
	varEnv.setObject(new Plane(varFloor));
	varEnv.setObject(new Plane(varLeftWall));
	varEnv.setObject(new Plane(varRightWall));
	varEnv.setObject(new Sphere(varObjMid));
	varEnv.setObject(new Sphere(varObjRight));
	varEnv.setObject(new Sphere(varObjLeft));
	// varEnv.setObject(std::make_unique<Sphere>(varFloor));
	// varEnv.setObject(std::make_unique<Sphere>(varLeftWall));
	// varEnv.setObject(std::make_unique<Sphere>(varRightWall));
	// varEnv.setObject(std::make_unique<Sphere>(varObjMid));
	// varEnv.setObject(std::make_unique<Sphere>(varObjRight));
	// varEnv.setObject(std::make_unique<Sphere>(varObjLeft));
	varEnv.mbrLights.push_back(varLightL);
	//varEnv.mbrLights.push_back(varLightR);

	Camera varCamera = Camera(100,50,getPI()/2);
	varCamera.setTransform(new ViewMatrix(Point(0,1.5,-20), Point(0,1,0), Vector(0,1,0)));

	Canvas img = varCamera.render(varEnv);
	img.save();
}

int main(int argc, char **argv)
{

	// double arr[] = { 2, 0, 0, 0, 0, 1, 0, 0, 0,0,1,0 ,0 ,0 ,0, 1 };
	// Matrix m = Matrix(4, 4, arr);
	// Tuple t = Tuple(2, 2, 2, 2);
	// Tuple x = m * t;

	// int firepower = 10;
	// std::cout << "Firing: " << firepower << std::endl;
	// Simulation sim;
	// sim.fire(firepower);

	// shadowTracer();

	// shadingTracerSphere(Point(0,0,-5), PointSource(Point(-10,10,-10),Color(1,1,1)), 10, 10, 100, Color(1, 0.2, 1));
	// shadingTracerSphere(Point(0,0,-5), PointSource(Point(-10,10,-10),Color(0.5,0.5,0.5)), 10, 10, 100, Color(1, 0.2, 1));

	// cameraRenderSpheres();
	// cameraRenderPlane();
	// cameraRenderPlanes();

	// cameraRenderSpherePlane();
	cameraRenderPatterns();

    ::testing::InitGoogleTest( &argc, argv);
    return RUN_ALL_TESTS();

	// 	return 0;
}