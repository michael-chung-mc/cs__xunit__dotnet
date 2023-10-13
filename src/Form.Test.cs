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
}
public class SphereTest
{
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void EqualityTest() {
		Sphere s = new Sphere();
		Sphere t = new Sphere();
		Assert.True(s.CheckEqual(t));
	}
    [Fact]
	public void RayIntersectTwo() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(4, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(6, varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void RayIntersectTangent() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(5 , varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(5 , varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void RayIntersectMiss() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Empty(varXs._fieldIntersections);
	}
    [Fact]
	public void RayIntersectWithinSphere() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, 0),new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2 , varXs._fieldIntersections.Count());
		Assert.Equal(-1 , varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(1 , varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void RayIntersectBehindSphere() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2 , varXs._fieldIntersections.Count());
		Assert.Equal(-6 , varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(-4 , varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
	public void RayIntersectSetsObject() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2 , varXs._fieldIntersections.Count());
		Assert.True(s.CheckEqual((varXs._fieldIntersections[0]._fieldObject)));
		Assert.True(s.CheckEqual((varXs._fieldIntersections[1]._fieldObject)));
	}
    [Fact]
	public void SphereDefaultTransformIsIdentity() {
		Sphere s = new Sphere();
		IdentityMatrix m = new IdentityMatrix(4, 4);
		Assert.True(m.CheckEqual(s._fieldTransform));
	}
    [Fact]
	public void SphereModifyTransform() {
		Sphere s = new Sphere();
		TranslationMatrix m = new TranslationMatrix(2, 3, 4);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
	}
    [Fact]
	public void SphereIdentityDoesNotModifyIntersections() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		ScalingMatrix m = new ScalingMatrix(1, 1, 1);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
		Intersections varXs = s.GetIntersections(r);
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.Equal(4, varXs._fieldIntersections[0]._fieldTime);
		Assert.Equal(6, varXs._fieldIntersections[1]._fieldTime);
	}
    [Fact]
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
    [Fact]
	public void SphereScaledTo5Intersections() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		ScalingMatrix m = new ScalingMatrix(5, 5, 5);
		s.SetTransform(m);
		Assert.True(m.CheckEqual(s._fieldTransform));
		Intersections varXs = s.GetIntersections(r);
		double z = 0;
		double y = 10;
		Assert.Equal(2, varXs._fieldIntersections.Count());
		Assert.True(_fieldComp.CheckFloat(varXs._fieldIntersections[0]._fieldTime, z));
		Assert.True(_fieldComp.CheckFloat(varXs._fieldIntersections[1]._fieldTime, y));
	}
    [Fact]
	public void SphereTranslatedToMiss() {
		Sphere s = new Sphere();
		Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
		TranslationMatrix m = new TranslationMatrix(5, 0, 0);
		s.SetTransform(new Matrix(m));
		Intersections varXs = s.GetIntersections(r);
		Assert.Empty(varXs._fieldIntersections);
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldOrigin, new Point(-5,0,-5)));
		Assert.True(_fieldComp.CheckTuple(s._fieldObjectRay._fieldDirection, new Vector(0,0,1)));
	}
    [Fact]
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
    [Fact]
	public void SphereNormalX() {
		Sphere s = new Sphere();
		Point p = new Point(1,0,0);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(1,0,0);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereNormalY() {
		Sphere s = new Sphere();
		Point p = new Point(0,1,0);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0,1,0);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereNormalZ() {
		Sphere s = new Sphere();
		Point p = new Point(0,0,1);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0,0,1);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereNormal() {
		Sphere s = new Sphere();
		Point p = new Point(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereNormalNormalized() {
		Sphere s = new Sphere();
		Point p = new Point(Math.Sqrt(3)/3,Math.Sqrt(3)/3,Math.Sqrt(3)/3);
		Vector normal = s.GetNormal(p);
		Vector expectedV = normal.GetNormal();
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereTranslatedNormalized() {
		Sphere s = new Sphere();
		Matrix t = new TranslationMatrix(0,1,0);
		s.SetTransform(t);
		Point p = new Point(0, 1.70711, -0.70711);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0, 0.70711, -0.70711);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}
    [Fact]
	public void SphereTransformedNormalized() {
		Sphere s = new Sphere();
		// Matrix t = *(ScalingMatrix(1, 0.5, 1) * ZRotationMatrix(getPI()/5));
		Matrix t = new ScalingMatrix(1, 0.5, 1) * new ZRotationMatrix(varPM.GetPI()/5);
		s.SetTransform(t);
		Point p = new Point(0,Math.Sqrt(2)/2,-Math.Sqrt(2)/2);
		Vector normal = s.GetNormal(p);
		Vector expectedV = new Vector(0,0.97014,-0.24254);
		Assert.True(_fieldComp.CheckTuple(normal,expectedV));
	}

    [Fact]
	public void SphereMaterialCtor() {
		Sphere s = new Sphere();
		Material m = new Material();
		Assert.True(m.CheckEqual(s._fieldMaterial));
	}

    [Fact]
	public void SphereMaterialAssignment() {
		Sphere s = new Sphere();
		Material m = new Material();
		m._fieldAmbient = 1;
		s.SetMaterial(m);
		Assert.True(m.CheckEqual(s._fieldMaterial));
	}
}
public class SphereGlassTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}

    [Fact]
	public void GlassSphereCtor() {
		SphereGlass varObj = new SphereGlass();
		Assert.True(varObj._fieldTransform.CheckEqual(new IdentityMatrix(4,4)));
		Assert.Equal(1.0, varObj._fieldMaterial._fieldTransparency);
		Assert.Equal(1.5, varObj._fieldMaterial._fieldRefractiveIndex);
	}
}
public class PlaneTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}

    [Fact]
	public void PlaneNormalSameEverywhere() {
		Plane varPlane = new Plane();
		Vector varN1 = varPlane.GetNormal(new Point(0,0,0));
		Vector varN2 = varPlane.GetNormal(new Point(10,0,-10));
		Vector varN3 = varPlane.GetNormal(new Point(-5,0,150));
		Assert.True(_fieldComp.CheckTuple(varN1, new Vector(0,1,0)));
		Assert.True(_fieldComp.CheckTuple(varN2, new Vector(0,1,0)));
		Assert.True(_fieldComp.CheckTuple(varN3, new Vector(0,1,0)));

	}

    [Fact]
	public void RayParallelToPlane() {
		Plane varPlane = new Plane();
		Ray varRay = new Ray(new Point(0,10,0), new Vector(0,0,1));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 0);
	}

    [Fact]
	public void CoplanarRayToPlane() {
		Plane varPlane = new Plane();
		Ray varRay = new Ray(new Point(0,0,0), new Vector(0,0,1));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 0);
	}
    [Fact]
	public void RayIntersectingPlaneAbove() {
		Plane varPlane = new Plane();
		Ray varRay = new Ray(new Point(0,1,0), new Vector(0,-1,0));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 1);
		Assert.Equal(1, varIx._fieldIntersections[0]._fieldTime);
		Assert.True(varIx._fieldIntersections[0]._fieldObject.CheckEqual(varPlane));
	}
    [Fact]
	public void RayIntersectingPlaneBelow() {
		Plane varPlane = new Plane();
		Ray varRay = new Ray(new Point(0,-1,0), new Vector(0,1,0));
		Intersections varIx = varPlane.GetIntersections(varRay);
		Assert.True(varIx._fieldIntersections.Count() == 1);
		Assert.Equal(1, varIx._fieldIntersections[0]._fieldTime);
		Assert.True(varIx._fieldIntersections[0]._fieldObject.CheckEqual(varPlane));
	}
}
public class AABBTest {
	Comparinator _fieldComp = new Comparinator();
	ProjectMeta varPM = new ProjectMeta();
    [Fact]
	public void CanaryTest() {
		Assert.Equal(1, 1);
		Assert.True(true);
	}
    [Fact]
	public void Ray_Intersect_Cube__Positive_X() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(5,0.5,0), new Vector(-1,0,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Negative_X() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(-5,0.5,0), new Vector(1,0,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Positive_Y() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(.5,5,0), new Vector(0,-1,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Negative_Y() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(.5,-5,0), new Vector(0,1,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Positive_Z() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(.5,0,5), new Vector(0,0,-1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Negative_Z() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(.5,0,-5), new Vector(0,0,1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(4, varXs[0]._fieldTime);
		Assert.Equal(6, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Intersect_Cube__Interior() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(0,0.5,0), new Vector(0,0,1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Equal(2, varXs.Count);
		Assert.Equal(-1, varXs[0]._fieldTime);
		Assert.Equal(1, varXs[1]._fieldTime);
	}
    [Fact]
	public void Ray_Miss_Cube__Away_X() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(-2,0,0), new Vector(0.2673,0.5345,0.8018));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Miss_Cube__Away_Y() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(0,-2,0), new Vector(0.8018,0.2673,0.5345));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Miss_Cube__Away_Z() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(0,0,-2), new Vector(0.5345, 0.8018,0.2673));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Miss_Cube__Parallel_X_Z() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(2,0,2), new Vector(0,0,-1));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Miss_Cube__Parallel_Y_Z() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(0,2,2), new Vector(0,-1,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Ray_Miss_Cube__Parallel_X_Y() {
		AABBox varCube = new AABBox();
		Ray varRay = new Ray(new Point(2,2,0), new Vector(-1,0,0));
		List<Intersection> varXs = varCube.GetIntersectionsLocal(varRay)._fieldIntersections;
		Assert.Empty(varXs);
	}
    [Fact]
	public void Cube_Normal__Positive_X() {
		AABBox varCube = new AABBox();
		Point varPoint = new Point(1,0.5,-0.8);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Negative_X() {
		AABBox varCube = new AABBox();
		Point varPoint = new Point(-1,-0.2,0.9);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(-1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Positive_Y() {
		AABBox varCube = new AABBox();
		Point varPoint = new Point(-0.4,1,-0.1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(0,1,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Negative_Y() {
		AABBox varCube = new AABBox();
		Point varPoint = new Point(0.3,-1,1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(0,-1,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Positive_Z() {
		AABBox varCube = new AABBox();
		Point varPoint = new Point(-0.6,0.3,1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(0,0,1);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Negative_Z() {
		AABBox varCube = new AABBox();
		Point varPoint = new Point(0.4,0.4,-1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(0,0,-1);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Corner_X() {
		AABBox varCube = new AABBox();
		Point varPoint = new Point(1,1,1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
    [Fact]
	public void Cube_Normal__Corner_Negative_X() {
		AABBox varCube = new AABBox();
		Point varPoint = new Point(-1,-1,-1);
		Vector varNormal = varCube.GetNormalLocal(varPoint);
		Vector varExpectedNormal = new Vector(-1,0,0);
		Assert.True(_fieldComp.CheckTuple(varExpectedNormal,varNormal));
	}
}