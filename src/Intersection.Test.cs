using Xunit;
using LibLight;
using LibForm;
using LibIntersection;
using LibComparinator;
using LibRay;
using LibTuple;
using LibPattern;
using LibProjectMeta;
using LibMaterial;
using LibMatrix;

namespace LibLight.Test;

public class IntersectionTest
{
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]

	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}

	public void InitTest() {
		Sphere s = new Sphere();
		double t = 3.5;
		// Intersection i = Intersection(t, std::make_unique<Sphere>(s));
		Intersection i = new Intersection(t, s);
		Assert.Equal(i.mbrTime, t);
		Assert.True(i.mbrObject.CheckEqual(s));
	}

	public void AggregationTest() {
		Sphere s = new Sphere();
		double t1 = 1;
		double t2 = 2;
		// Intersections i = new Intersections(t1, std::make_unique<Sphere>(s));
		// i.intersect(t2, std::make_unique<Sphere>(s));
		Intersections i = new Intersections(t1, s);
		i.intersect(t2, s);
		// std::vector<Intersection> xs = i.mbrIntersections;
		// i.intersect(t2, s);
		// std::vector<std::unique_ptr<Intersection>> xs = i.mbrIntersections;
		Assert.Equal(i.mbrIntersections.Count(),2);
		// Assert.Equal(xs[0].mbrTime, t1);
		// Assert.Equal(xs[1].mbrTime, t2);
		Assert.Equal(i.mbrIntersections[0].mbrTime, t1);
		Assert.Equal(i.mbrIntersections[1].mbrTime, t2);
	}

	public void PositiveT() {
		Sphere s = new Sphere();
		Intersections i = new Intersections(1, s);
		// Intersections i = new Intersections(1, std::make_unique<Sphere>(s));
		// i.intersect(2, std::make_unique<Sphere>(s));
		i.intersect(2, s);
		// std::vector<std::unique_ptr<Intersection>> xs = i.mbrIntersections;
		Intersection hit = i.hit();
		// Assert.True(hit.checkEqual(xs[0]));
		Assert.True(hit.checkEqual(i.mbrIntersections[0]));
	}

	public void NegativeT() {
		Sphere s = new Sphere();
		Intersections i = new Intersections(-1, s);
		// Intersections i = new Intersections(-1, std::make_unique<Sphere>(s));
		// i.intersect(2, std::make_unique<Sphere>(s));
		i.intersect(2, s);
		// std::vector<Intersection> xs = i.mbrIntersections;
		// std::vector<std::unique_ptr<Intersection>> xs = i.mbrIntersections;
		Intersection hit = i.hit();
		// Assert.True(hit.checkEqual(xs[1]));
		Assert.True(hit.checkEqual(i.mbrIntersections[1]));
	}

	public void AllNegativeT() {
		Sphere s = new Sphere();
		// Intersections i = new Intersections(-1, std::make_unique<Sphere>(s));
		Intersections i = new Intersections(-1, s);
		// i.intersect(-2, std::make_unique<Sphere>(s));
		i.intersect(-2, s);
		// std::vector<Intersection> xs = i.mbrIntersections;
		// std::vector<std::unique_ptr<Intersection>> xs = i.mbrIntersections;
		Intersection hit = i.hit();
		Assert.False(hit.mbrExists);
	}

	public void HitvarIsNonnegativeIntersection() {
		Sphere s = new Sphere();
		// Intersections i = new Intersections(5, std::make_unique<Sphere>(s));
		Intersections i = new Intersections(5, s);
		i.intersect(7, s);
		// i.intersect(7, std::make_unique<Sphere>(s));
		i.intersect(-3, s);
		// i.intersect(-3, std::make_unique<Sphere>(s));
		// i.intersect(2, std::make_unique<Sphere>(s));
		i.intersect(2, s);
		// std::vector<Intersection> xs = i.mbrIntersections;
		// std::vector<std::unique_ptr<Intersection>> xs = i.mbrIntersections;
		Intersection hit = i.hit();
		// Assert.True(hit.checkEqual(xs[1]));
		Assert.True(hit.checkEqual(i.mbrIntersections[1]));
	}

	public void PrecomputeIntersectionState() {
		Ray r = new Ray(new Point(0,0,-5), new Vector(0,0,1));
		Sphere s = new Sphere();
		// Intersection i = Intersection(4, std::make_unique<Sphere>(s));
		// Intersection i = Intersection(4, new Sphere(s));
		// IntersectionState varIs = i.getState(r);
		Intersections i = new Intersections(4, s);
		IntersectionState varIs = i.hit().getState(r, i.mbrIntersections);
		Assert.True(_fieldComp.CheckFloat(varIs.mbrTime, i.hit().mbrTime));
		Assert.True(varIs.mbrObject.CheckEqual(i.hit().mbrObject));
		Assert.True(_fieldComp.CheckTuple(varIs.mbrPoint, new Point(0,0,-1)));
		Assert.True(_fieldComp.CheckTuple(varIs.mbrEye, new Vector(0,0,-1)));
		Assert.True(_fieldComp.CheckTuple(varIs.mbrNormal, new Vector(0,0,-1)));
	}

	public void PrecomputeIntersectionStateInteriorHitFalse() {
		Ray r = new Ray(new Point(0,0,-5), new Vector(0,0,1));
		Sphere s = new Sphere();
		// Intersection i = Intersection(4, std::make_unique<Sphere>(s));
		// Intersection i = Intersection(4, new Sphere (s));
		// IntersectionState varIs = i.getState(r);
		Intersections i = new Intersections(4, s);
		IntersectionState varIs = i.hit().getState(r, i.mbrIntersections);
		Assert.False(varIs.mbrInside);
	}

	public void PrecomputeIntersectionStateInteriorHitTrue() {
		Ray r = new Ray(new Point(0,0,0), new Vector(0,0,1));
		Sphere s = new Sphere();
		// Intersection i = Intersection(1, std::make_unique<Sphere>(s));
		// Intersection i = Intersection(1, new Sphere(s));
		Intersections i = new Intersections(1, s);
		// IntersectionState varIs = i.getState(r);
		IntersectionState varIs = i.hit().getState(r, i.mbrIntersections);
		Assert.True(_fieldComp.CheckTuple(varIs.mbrPoint, new Point(0,0,1)));
		Assert.True(_fieldComp.CheckTuple(varIs.mbrEye, new Vector(0,0,-1)));
		Assert.True(varIs.mbrInside);
		Assert.True(_fieldComp.CheckTuple(varIs.mbrNormal, new Vector(0,0,-1)));
	}

	public void HitShouldOffsetPoint() {
		Ray varRay = new Ray(new Point(0,0,-5), new Vector(0,0,1));
		Sphere varSphere = new Sphere();
		varSphere.SetTransform(new TranslationMatrix(0,0,1));
		// Intersection varIntersection = Intersection(5, std::make_unique<Sphere>(varSphere));
		// Intersection varIntersection = Intersection(5, new Sphere(varSphere));
		Intersections varIntersection = new Intersections(5, varSphere);
		// IntersectionState varvarIs = varIntersection.getState(varRay);
		IntersectionState varvarIs = varIntersection.hit().getState(varRay, varIntersection.mbrIntersections);
		Assert.True(varvarIs.mbrOverPoint._fieldZ < -varPM.getEpsilon()/2);
		Assert.True(varvarIs.mbrPoint._fieldZ > varvarIs.mbrOverPoint._fieldZ);
	}

	public void ReflectionIntersectionState()
	{
		Plane varObj = new Plane();
		Ray varRay = new Ray(new Point(0,1,-1), new Vector(0,-Math.Sqrt(2)/2, Math.Sqrt(2)/2));
		// Intersection varIx = Intersection(Math.Sqrt(2), new Plane(varObj));
		Intersections varIx = new Intersections(Math.Sqrt(2), varObj);
		// IntersectionState varvarIs = varIx.getState(varRay);
		IntersectionState varvarIs = varIx.hit().getState(varRay, varIx.mbrIntersections);
		Assert.True(_fieldComp.CheckTuple(varvarIs.mbrReflect, new Vector(0,Math.Sqrt(2)/2, Math.Sqrt(2)/2)));
	}

	public void RefractiveIndex () {
		SphereGlass varSphereA = new SphereGlass();
		varSphereA.SetTransform(new ScalingMatrix(2,2,2));
		varSphereA._fieldMaterial.mbrRefractiveIndex = 1.5;
		SphereGlass varSphereB = new SphereGlass();
		varSphereB.SetTransform(new TranslationMatrix(0,0,-0.25));
		varSphereB._fieldMaterial.mbrRefractiveIndex = 2.0;
		SphereGlass varSphereC = new SphereGlass();
		varSphereC.SetTransform(new TranslationMatrix(0,0,0.25));
		varSphereC._fieldMaterial.mbrRefractiveIndex = 2.5;
		Ray argRay = new Ray(new Point(0,0,-4), new Vector(0,0,1));
		Intersections varvarIs = new Intersections();
		varvarIs.intersect(2, new SphereGlass(varSphereA));
		varvarIs.intersect(2.75, new SphereGlass(varSphereB));
		varvarIs.intersect(3.25, new SphereGlass(varSphereC));
		varvarIs.intersect(4.75, new SphereGlass(varSphereB));
		varvarIs.intersect(5.25, new SphereGlass(varSphereC));
		varvarIs.intersect(6, new SphereGlass(varSphereA));
		// IntersectionState varIx0 = varvarIs.mbrIntersections[0].getState(argRay, varvarIs);
		// Assert.True(_fieldComp.CheckFloat(varIx0.mbrRefractiveIndexOne, 1.0));
		// Assert.True(_fieldComp.CheckFloat(varIx0.mbrRefractiveIndexTwo, 1.5));
		// IntersectionState varIx1 = varvarIs.mbrIntersections[1].getState(argRay, varvarIs);
		// Assert.True(_fieldComp.CheckFloat(varIx1.mbrRefractiveIndexOne, 1.5));
		// Assert.True(_fieldComp.CheckFloat(varIx1.mbrRefractiveIndexTwo, 2.0));
		// IntersectionState varIx2 = varvarIs.mbrIntersections[2].getState(argRay, varvarIs);
		// Assert.True(_fieldComp.CheckFloat(varIx2.mbrRefractiveIndexOne, 2.0));
		// Assert.True(_fieldComp.CheckFloat(varIx2.mbrRefractiveIndexTwo, 2.5));
		// IntersectionState varIx3 = varvarIs.mbrIntersections[3].getState(argRay, varvarIs);
		// Assert.True(_fieldComp.CheckFloat(varIx3.mbrRefractiveIndexOne, 2.5));
		// Assert.True(_fieldComp.CheckFloat(varIx3.mbrRefractiveIndexTwo, 2.5));
		// IntersectionState varIx4 = varvarIs.mbrIntersections[4].getState(argRay, varvarIs);
		// Assert.True(_fieldComp.CheckFloat(varIx4.mbrRefractiveIndexOne, 2.5));
		// Assert.True(_fieldComp.CheckFloat(varIx4.mbrRefractiveIndexTwo, 1.5));
		// IntersectionState varIx5 = varvarIs.mbrIntersections[5].getState(argRay, varvarIs);
		// Assert.True(_fieldComp.CheckFloat(varIx5.mbrRefractiveIndexOne, 1.5));
		// Assert.True(_fieldComp.CheckFloat(varIx5.mbrRefractiveIndexTwo, 1.0));
		IntersectionState varIx0 = varvarIs.mbrIntersections[0].getState(argRay, varvarIs.mbrIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx0.mbrRefractiveIndexOne, 1.0));
		Assert.True(_fieldComp.CheckFloat(varIx0.mbrRefractiveIndexTwo, 1.5));
		IntersectionState varIx1 = varvarIs.mbrIntersections[1].getState(argRay, varvarIs.mbrIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx1.mbrRefractiveIndexOne, 1.5));
		Assert.True(_fieldComp.CheckFloat(varIx1.mbrRefractiveIndexTwo, 2.0));
		IntersectionState varIx2 = varvarIs.mbrIntersections[2].getState(argRay, varvarIs.mbrIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx2.mbrRefractiveIndexOne, 2.0));
		Assert.True(_fieldComp.CheckFloat(varIx2.mbrRefractiveIndexTwo, 2.5));
		IntersectionState varIx3 = varvarIs.mbrIntersections[3].getState(argRay, varvarIs.mbrIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx3.mbrRefractiveIndexOne, 2.5));
		Assert.True(_fieldComp.CheckFloat(varIx3.mbrRefractiveIndexTwo, 2.5));
		IntersectionState varIx4 = varvarIs.mbrIntersections[4].getState(argRay, varvarIs.mbrIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx4.mbrRefractiveIndexOne, 2.5));
		Assert.True(_fieldComp.CheckFloat(varIx4.mbrRefractiveIndexTwo, 1.5));
		IntersectionState varIx5 = varvarIs.mbrIntersections[5].getState(argRay, varvarIs.mbrIntersections);
		Assert.True(_fieldComp.CheckFloat(varIx5.mbrRefractiveIndexOne, 1.5));
		Assert.True(_fieldComp.CheckFloat(varIx5.mbrRefractiveIndexTwo, 1.0));
	}

	public void UnderPointUnderSurfaceByEpsilon ()
	{
		Ray varRay = new Ray(new Point(0,0,-5), new Vector(0,0,1));
		SphereGlass varObj = new SphereGlass();
		varObj.SetTransform(new TranslationMatrix(0,0,1));
		Intersections varIx = new Intersections(5, new SphereGlass(varObj));
		IntersectionState varvarIs = varIx.hit().getState(varRay, varIx.mbrIntersections);
		Assert.True(varvarIs.mbrUnderPoint._fieldZ > varPM.getEpsilon()/2);
		Assert.True(varvarIs.mbrPoint._fieldZ < varvarIs.mbrUnderPoint._fieldZ);
	}
}