using LibMatrix;
using LibColor;
using LibTuple;
namespace LibPattern;

public class Pattern {
    public Color _fieldBlack;
    public Color _fieldWhite;
    public List<Color> _fieldColors;
    public Matrix _fieldTransform;
    public Matrix _fieldTransformInverse;
    public Pattern()
    {
        _fieldColors = new List<Color>();
        _fieldWhite = new Color(1,1,1);
        _fieldBlack = new Color(0,0,0);
        SetTransform(new IdentityMatrix(4,4));
    }
	public Pattern(Pattern other)
    {
        _fieldWhite = new Color(1,1,1);
        _fieldBlack = new Color(0,0,0);
        _fieldColors = new List<Color>();
        _fieldColors = other._fieldColors;
        SetTransform(other._fieldTransform);
    }
    public Pattern(Color argColorA, Color argColorB) {
        _fieldWhite = new Color(1,1,1);
        _fieldBlack = new Color(0,0,0);
        _fieldColors = new List<Color>{argColorA, argColorB};
        SetTransform(new IdentityMatrix(4,4));
    }
    public Color GetColor(Matrix argObjTransformInverse, Point argPoint) {
		Point varObjP = argObjTransformInverse * argPoint;
		Point varPatternP = this._fieldTransformInverse * varObjP;
        return GetColorLocal(varPatternP);
    }
    public virtual Color GetColorLocal(Point argPoint) {
        return new Color(argPoint._fieldX, argPoint._fieldY, argPoint._fieldZ);
    }
    public virtual bool CheckEqual(Pattern argOther) {
        var varFirstNotSecond = this._fieldColors.Except(argOther._fieldColors).ToList();
        var varSecondNotFirst = argOther._fieldColors.Except(this._fieldColors).ToList();
        return !varFirstNotSecond.Any() && !varSecondNotFirst.Any()
            && this._fieldTransform.CheckEqual(argOther._fieldTransform)
            && this._fieldTransformInverse.CheckEqual(argOther._fieldTransformInverse);
    }
    public void SetTransform(Matrix argMatrix){
        _fieldTransform = argMatrix;
        _fieldTransformInverse = argMatrix.GetInverse();
    }
    public void RenderConsole() {
        Console.WriteLine("rPattern::enderConsole()");
        Console.WriteLine("Pattern::_fieldColors(");
        for (int i = 0; i < 0; ++i) {
            _fieldColors[i].RenderConsole();
        }
        Console.WriteLine("Pattern::_fieldTransform(");
        _fieldTransform.RenderConsole();
    }
};

public class PatternStripe : Pattern {
    public PatternStripe(){
        _fieldColors.Add(_fieldWhite);
        _fieldColors.Add(_fieldBlack);
    }
	public PatternStripe(PatternStripe other)
    {
        _fieldColors = other._fieldColors;
        SetTransform(other._fieldTransform);
    }
    public PatternStripe(Color argColorA, Color argColorB){
        _fieldColors.Add(argColorA);
        _fieldColors.Add(argColorB);
    }
    public override Color GetColorLocal(Point argPoint) {
        return (int)(Math.Floor(argPoint._fieldX) % 2) == 0 ? _fieldColors[0] : _fieldColors[1];
    }
};

public class PatternGradient : Pattern {
    public PatternGradient() {
        _fieldColors.Add(_fieldWhite);
        _fieldColors.Add(_fieldBlack);
    }
	public PatternGradient(PatternGradient other)
    {
        _fieldColors = other._fieldColors;
        SetTransform(other._fieldTransform);
    }
    public PatternGradient(Color argColorA, Color argColorB){
        _fieldColors.Add(argColorA);
        _fieldColors.Add(argColorB);
    }
    public override Color GetColorLocal(Point argPoint){
        return GetColorGradientBasicLerpOffsetHalf(argPoint);
    }
    public Color GetColorGradientBasicLerp(Point argPoint){
        // c = (1-t) * a + t * b == c = a + t(b-a)
        return _fieldColors[0] + (_fieldColors[1]-_fieldColors[0]) * (argPoint._fieldX - Math.Floor(argPoint._fieldX));
    }
    public Color GetColorGradientBasicLerpLimit(Point argPoint){
        // c = (1-t) * a + t * b == c = a + t(b-a)
        var varProportion = 1-argPoint._fieldX;
        return _fieldColors[0] + (_fieldColors[1] - _fieldColors[0]) * varProportion;
    }
    public Color GetColorGradientBasicLerpOffsetHalf(Point argPoint){
        var varProportion = (argPoint._fieldX + 1.0) * 0.5;
        return _fieldColors[0] + (_fieldColors[1] - _fieldColors[0]) * varProportion;
    }
};

public class PatternRing : Pattern {
    public PatternRing() {
        _fieldColors.Add(_fieldWhite);
        _fieldColors.Add(_fieldBlack);
    }
	public PatternRing(PatternRing other) {
        _fieldColors = other._fieldColors;
        SetTransform(other._fieldTransform);
    }
    public PatternRing(Color argColorA, Color argColorB) {
        _fieldColors.Add(argColorA);
        _fieldColors.Add(argColorB);
    }
    public override Color GetColorLocal(Point argPoint) {
        return (int)(Math.Floor(Math.Sqrt((argPoint._fieldX*argPoint._fieldX) + (argPoint._fieldZ * argPoint._fieldZ)))%2) == 0 ? _fieldColors[0]: _fieldColors[1];
    }
};

public class PatternChecker : Pattern {
    public PatternChecker() {
        _fieldColors.Add(_fieldWhite);
        _fieldColors.Add(_fieldBlack);
    }
	public PatternChecker(PatternChecker other) {
        _fieldColors = other._fieldColors;
        SetTransform(other._fieldTransform);
    }
    public PatternChecker(Color argColorA, Color argColorB) {
        _fieldColors.Add(argColorA);
        _fieldColors.Add(argColorB);
    }
    public override Color GetColorLocal(Point argPoint) {
        return (int)((Math.Floor(argPoint._fieldX) + Math.Floor(argPoint._fieldY) + Math.Floor(argPoint._fieldZ)))%2 == 0 ? _fieldColors[0]: _fieldColors[1];
    }
};