// Fire: Vf^2 = Vi^2 + 2aΔX
public class Fire : KinematicEquation {

   public static readonly string equation = "Vf^2 = Vi^2 + 2aΔX";
   public static readonly bool[] presentQuantities = {true, true, false, true, true};

   public Fire(bool [] knownQuantities, string[] quantities) {
		absentQuantityIndex = 2;
		SetKnownQuantities(knownQuantities);
		SetQuantities(quantities);

		SetLeftSide(new BinaryExpression(new UnaryExpression(GetQuantity(1)), new UnaryExpression("2"), '^'));
		SetRightSide(new BinaryExpression(new BinaryExpression(new UnaryExpression(GetQuantity(0)), new UnaryExpression("2"), '^'), new BinaryExpression(new UnaryExpression(GetQuantity(3)), new BinaryExpression(new UnaryExpression(GetQuantity(4)), new UnaryExpression("2"), '*'), '*'), '+'));

		CheckNumberOfQuantities(NumberOfKnownQuantities());
   }

   override
   public void DoAlgebra() {

		SetWork(new Steps(equation));
		Algebra.solveEquation(false, this.work, leftSide, rightSide, GetMissingQuantityIndex());

		SetQuantity(GetMissingQuantityIndex(), this.work.GetNumericalAnswer().ToString()); //adds answer to array and updates knowns
    }
}
