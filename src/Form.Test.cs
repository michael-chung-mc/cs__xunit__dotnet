using Xunit;
using LibForm;
using LibTuple;
using LibRay;
using LibMaterial;
using LibMatrix;
using LibComparinator;
using LibIntersection;
using LibProjectMeta;

namespace LibForm.Test;

public class FormTest
{
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}

	public void EqualityTest() {
		Sphere s = new Sphere();
		Sphere t = new Sphere();
		Assert.True(s.CheckEqual(t));
	}

	public void RayIntersectTwo() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		// std::vector<Intersection> xs = s.getIntersections(r).mbrIntersections;
		Intersections varXs = s.GetIntersections(r);
		// Assert.Equal(xs.Count(), 2);
		// Assert.Equal(xs[0].mbrTime, 4);
		// Assert.Equal(xs[1].mbrTime, 6);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(4, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(6, varXs._fieldIntersections[1]._fieldTime);
	}

	public void RayIntersectTangent() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));
		// std::vector<Intersection> xs = s.getIntersections(r).mbrIntersections;
		Intersections varXs = s.GetIntersections(r);
		// Assert.Equal(xs.Count(), 2);
		// Assert.Equal(xs[0].mbrTime, 5);
		// Assert.Equal(xs[1].mbrTime, 5);
		Assert.Equal(varXs._fieldIntersections.Count(), 2);
		Assert.Equal(varXs._fieldIntersections[0]._fieldTime, 5);
		Assert.Equal(varXs._fieldIntersections[1]._fieldTime, 5);
	}

	public void RayIntersectMiss() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));
		// std::vector<Intersection> xs = s.getIntersections(r).mbrIntersections;
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(varXs._fieldIntersections.Count(), 0);
	}

	public void RayIntersectWithinSphere() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, 0),new Vector(0, 0, 1));
		// std::vector<Intersection> xs = s.getIntersections(r).mbrIntersections;
		// Assert.Equal(xs.Count(), 2);
		// Assert.Equal(xs[0].mbrTime, -1);
		// Assert.Equal(xs[1].mbrTime, 1);
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(varXs._fieldIntersections.Count(), 2);
		Assert.Equal(varXs._fieldIntersections[0]._fieldTime, -1);
		Assert.Equal(varXs._fieldIntersections[1]._fieldTime, 1);
	}

	public void RayIntersectBehindSphere() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
		// std::vector<Intersection> xs = s.getIntersections(r).mbrIntersections;
		// Assert.Equal(xs.Count(), 2);
		// Assert.Equal(xs[0].mbrTime, -6);
		// Assert.Equal(xs[1].mbrTime, -4);
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(varXs._fieldIntersections.Count(), 2);
		Assert.Equal(varXs._fieldIntersections[0]._fieldTime, -6);
		Assert.Equal(varXs._fieldIntersections[1]._fieldTime, -4);
	}

	public void RayIntersectSetsObject() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		// std::vector<Intersection> xs = s.getIntersections(r).mbrIntersections;
		// Assert.Equal(xs.Count(), 2);
		// Assert.True(s.CheckEqual(xs[0].mbrObject));
		// Assert.True(s.CheckEqual(xs[1].mbrObject));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(varXs._fieldIntersections.Count(), 2);
		Assert.True(s.CheckEqual((varXs._fieldIntersections[0]._fieldObject)));
		Assert.True(s.CheckEqual((varXs._fieldIntersections[1]._fieldObject)));
	}

	public void SphereDefaultTransformIsIdentity() {
		Sphere s = new Sphere();
		IdentityMatrix m = new IdentityMatrix(4, 4);
		Assert.True(m.CheckEqual(s._fieldTransform));
	}

	public void SphereModifyTransform() {
		Sphere s = new Sphere();
		TranslationMatrix m = new TranslationMatrix(2, 3, 4);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
	}

	public void SphereIdentityDoesNotModifyIntersections() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		ScalingMatrix m = new ScalingMatrix(1, 1, 1);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
		// std::vector<Intersection> xs = s.getIntersections(r).mbrIntersections;
		// Assert.Equal(xs.Count(), 2);
		// Assert.Equal(xs[0].mbrTime, 4);
		// Assert.Equal(xs[1].mbrTime, 6);
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(4, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(6, varXs._fieldIntersections[1]._fieldTime);
	}

	public void SphereScaledModifiesIntersections() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		ScalingMatrix m = new ScalingMatrix(2, 2, 2);
		s.SetTransform(m);
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(3, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(7, varXs._fieldIntersections[1]._fieldTime);
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldOrigin, new Point(0,0,-2.5)));
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldDirection, new Vector(0,0,0.5)));
	}

	public void SphereScaledTo5Intersections() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		ScalingMatrix m = new ScalingMatrix(5, 5, 5);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
		// std::vector<Intersection> xs = s.getIntersections(r).mbrIntersections;
		Intersections varXs = s.GetIntersections(r);
		double z = 0;
		double y = 10;
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.True(_fieldComp.CheckFloat(varXs._fieldIntersections[0]._fieldTime, z));
		Assert.True(_fieldComp.CheckFloat(varXs._fieldIntersections[1]._fieldTime, y));
	}

	public void SphereTranslatedToMiss() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		TranslationMatrix m = new TranslationMatrix(5, 0, 0);
		s.SetTransform(new Matrix(m));
		// std::vector<Intersection> xs = s.getIntersections(r).mbrIntersections;
		Intersections varXs = s.GetIntersections(r);
		// Assert.Equal(xs.Count(), 0);
		Assert.Equal(varXs._fieldIntersections.Count(), 0);
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldOrigin, new Point(-5,0,-5)));
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldDirection, new Vector(0,0,1)));
	}

	public void SphereTranslatedAway() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		TranslationMatrix m = new TranslationMatrix(0, 0, 1);
		s.SetTransform(m);
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(5, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(7, varXs._fieldIntersections[1]._fieldTime);
	}

	public void SphereNormalX() {
		Sphere s = new Sphere();
		Point p = new Point(1,0,0);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(1,0,0);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}

	public void SphereNormalY() {
		Sphere s = new Sphere();
		Point p = new Point(0,1,0);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0,1,0);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}

	public void SphereNormalZ() {
		Sphere s = new Sphere();
		Point p = new Point(0,0,1);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0,0,1);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}

	public void SphereNormal() {
		Sphere s = new Sphere();
		Point p = new Point(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}

	public void SphereNormalNormalized() {
		Sphere s = new Sphere();
		Point p = new Point(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		Vector normal = s.GetNormal(p);
		Vector expectedV = normal.GetNormal();
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}

	public void SphereTranslatedNormalized() {
		Sphere s = new Sphere();
		Matrix t = new TranslationMatrix(0,1,0);
		s.SetTransform(t);
		Point p = new Point(0, 1.70711, -0.70711);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0, 0.70711, -0.70711);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}

	public void SphereTransformedNormalized() {
		Sphere s = new Sphere();
		// Matrix t = *(ScalingMatrix(1, 0.5, 1) * ZRotationMatrix(getPI()/5));
		Matrix t = new ScalingMatrix(1, 0.5, 1) * new ZRotationMatrix(varPM.getPI()/5);
		s.SetTransform(t);
		Point p = new Point(0,Math.Sqrt(2)/2,-Math.Sqrt(2)/2);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0,0.97014,-0.24254);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}

	public void SphereMaterialCtor() {
		Sphere s = new Sphere();
		Material m = new Material();
		Assert.True(m.CheckEqual(s._fieldMaterial));
	}

	public void SphereMaterialAssignment() {
		Sphere s = new Sphere();
		Material m = new Material();
		m._fieldAmbient = 1;
		s.SetMaterial(m);
		Assert.True(m.CheckEqual(s._fieldMaterial));
	}

	public void GlassSphereCtor() {
		SphereGlass varObj = new SphereGlass();
		Assert.True(varObj._fieldTransform.CheckEqual(new IdentityMatrix(4,4)));
		Assert.Equal(1.0, varObj._fieldMaterial._fieldTransparency);
		Assert.Equal(1.5, varObj._fieldMaterial._fieldRefractiveIndex);
	}

	public void PlaneNormalSameEverywhere() {
		Plane varPlane = new Plane();
		Vector varN1 = varPlane.GetNormal(new Point(0,0,0));
		Vector varN2 = varPlane.GetNormal(new Point(10,0,-10));
		Vector varN3 = varPlane.GetNormal(new Point(-5,0,150));
		Assert.True(_fieldComp.CheckTuple(varN1, new Vector(0,1,0)));
		Assert.True(_fieldComp.CheckTuple(varN2, new Vector(0,1,0)));
		Assert.True(_fieldComp.CheckTuple(varN3, new Vector(0,1,0)));

	}
	public void RayParallelToPlane() {
		Plane varPlane = new Plane();
		Ray varRay = new Ray(new Point(0,10,0), new Vector(0,0,1));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 0);
	}
	public void CoplanarRayToPlane() {
		Plane varPlane = new Plane();
		Ray varRay = new Ray(new Point(0,0,0), new Vector(0,0,1));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 0);
	}
	public void RayIntersectingPlaneAbove() {
		Plane varPlane = new Plane();
		Ray varRay = new Ray(new Point(0,1,0), new Vector(0,-1,0));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 1);
		Assert.Equal(1, varIx._fieldIntersections[0]._fieldTime);
		Assert.True(varIx._fieldIntersections[0]._fieldObject.CheckEqual(varPlane));
	}
	public void RayIntersectingPlaneBelow() {
		Plane varPlane = new Plane();
		Ray varRay = new Ray(new Point(0,-1,0), new Vector(0,1,0));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 1);
		Assert.Equal(1, varIx._fieldIntersections[0]._fieldTime);
		Assert.True(varIx._fieldIntersections[0]._fieldObject.CheckEqual(varPlane));
	}
}