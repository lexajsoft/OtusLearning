using System;

namespace Code.Infrastructure.Services.Input
{
  public interface IInputService
  {
    event Action<int> OnUnitSelected;
    void UnitSelect(int unitId);
  }
}