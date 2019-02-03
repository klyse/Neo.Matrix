using System;

namespace NeoMatrix
{
	/// <summary>
	/// Implement this interface to allow matrix calculations like <see cref="MatrixValueExtensions.GetTotalSum{TMatrixValueType,TCalcValueType}"/>
	/// </summary>
	/// <typeparam name="TValueType"></typeparam>
	public interface IMatrixValue<out TValueType>
	{
		/// <summary>
		/// Returns value object of custom type
		/// </summary>
		/// <returns></returns>
		TValueType GetValueObject();
	}
}