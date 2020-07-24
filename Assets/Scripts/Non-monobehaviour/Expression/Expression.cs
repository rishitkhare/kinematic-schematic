public abstract class Expression {
    private bool isKnown;

    public void SetIsKnown(bool isKnown) {
        this.isKnown = isKnown;
    }

    public bool GetIsKnown() {
        return isKnown;
    }

    override
    public abstract string ToString();

    public abstract float Evaluate();
}
