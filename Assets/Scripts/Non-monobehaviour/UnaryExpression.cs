public class UnaryExpression : Expression {

    private string value;

    public UnaryExpression(string value) {
        this.value = value;
        if (Algebra.IsNumber(value)) {
            SetIsKnown(true);
        }
    }

    override
    public string ToString() {
        return value;
    }

    override
    public float Evaluate() {
        // Only runs when isKnown
        return float.Parse(value);
    }
}