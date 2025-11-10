using System;

namespace Code.Infrastructure.Services.Input
{
  public class InputService : IInputService
  {
    public event Action<int> OnUnitSelected;
    
    public void UnitSelect(int unitId) => 
      OnUnitSelected?.Invoke(unitId);
  }
}