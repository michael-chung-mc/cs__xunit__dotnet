using LibForm;
namespace LibConstructiveSolidGeometry;

public struct CSGOperation {
	public bool _fieldUnion;
	public bool _fieldIntersection;
	public bool _fieldDifference;
    public CSGOperation() {
        _fieldUnion = false;
        _fieldIntersection = false;
        _fieldDifference = false;
    }
}

public class ConstructiveSolidGeometry : Form {
    public Form _fieldLeft;
    public Form _fieldRight;
    public CSGOperation _fieldOperation;
    public ConstructiveSolidGeometry(CSGOperation argOperation, Form argLeft, Form argRight) {
        _fieldOperation = argOperation;
        _fieldLeft = argLeft;
        _fieldLeft._fieldParent = this;
        _fieldRight = argRight;
        _fieldRight._fieldParent = this;
    }
}