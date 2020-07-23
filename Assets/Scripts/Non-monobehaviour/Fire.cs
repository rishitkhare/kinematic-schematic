// Fire: Vf^2 = Vi^2 + 2aΔX
public class Fire : KinematicEquation {

   public static readonly string equation = "Vf^2 = Vi^2 + 2aΔX";
   public static readonly bool[] presentQuantities = {true, true, false, true, true};

   public Fire(bool [] knownQuantities, string[] quantities) {
	super.absentQuantityIndex = 2;
	setKnownQuantities(knownQuantities);
	setQuantities(quantities);

	setLeftSide(new BinaryExpression(new UnaryExpression(getQuantity(1)), new UnaryExpression("2"), '^'));
	setRightSide(new BinaryExpression(new BinaryExpression(new UnaryExpression(getQuantity(0)), new UnaryExpression("2"), '^'), new BinaryExpression(new UnaryExpression(getQuantity(3)), new BinaryExpression(new UnaryExpression(getQuantity(4)), new UnaryExpression("2"), '*'), '*'), '+'));

	checkNumberOfQuantities(numberOfKnownQuantities());
    }

   override
   public void doAlgebra() {

	setWork(new Steps(equation));
	Algebra.solveEquation(false, this.work, leftSide, rightSide, getMissingQuantityIndex());

	setQuantity(getMissingQuantityIndex(), Double.toString(this.work.getNumericalAnswer())); //adds answer to array and updates knowns
    }
}
