using Inventory.Components;

namespace Inventory
{
    public partial class Item
    {
        public void DropAnyOne()
        {
            if (HasComponent<DurabilityComponent>())
            {
                DropDurabilityOne();
                return;
            }
            if (HasComponent<StackableComponent>())
            {
                DropOneFromStack();
                return;
            }
        }
        
        public void DropAny(int count)
        {
            if (HasComponent<DurabilityComponent>())
            {
                DropDurability(count);
                return;
            }
            if (HasComponent<StackableComponent>())
            {
                DropSomeFromStack(count);
                return;
            }
        }
        

        // скинуть 1 элемент из стакнутого объекта
        public bool DropOneFromStack()
        {
            return DropSomeFromStack(1);
        }

        public bool DropSomeFromStack(int count = 1)
        {
            if (HasComponent<StackableComponent>())
            {
                var stackableComponent = GetComponent<StackableComponent>();
                if (stackableComponent.IsCanMinus(count))
                {
                    stackableComponent.Minus(count);
                    return true;
                }
            }

            return false;
        }

        public bool DropDurabilityOne()
        {
            return DropDurability(1);
        }

        public bool DropDurability(int count = 1)
        {
            if (HasComponent<DurabilityComponent>())
            {
                var stackableComponent = GetComponent<DurabilityComponent>();
                if (!stackableComponent.IsBroken) 
                {
                    stackableComponent.ReduceDurability(count);
                    return true;
                }
            }
            return false;
        }

        // проверка что предмет в принципе юзабелньый
        public bool IsUsableItem()
        {
            return HasComponent<UsableComponent>();
        }

        // проверка на то что предмет можно использовать
        public bool IsCanUseItem()
        {
            if (HasComponent<UsableComponent>())
            {
                // если предмет стакаемый значит надо проверить что там что то еще есть пед использованием
                if (HasComponent<StackableComponent>())
                {
                    return GetComponent<StackableComponent>().IsAny;
                }
                
                // если предмет использует прочность то надо проверить что предмет не сломан/ не исчерпан
                if (HasComponent<DurabilityComponent>())
                {
                    return !GetComponent<DurabilityComponent>().IsBroken;
                }

                // TODO тут появилась еще идея добавить cooldownComponent но потом уже стало лень реализовывать
                
                // если ничего такого нет значит можно юзать ВСЕГДА
                return true;
            }

            return false;
        }

        public void UseItem()
        {
            if (IsCanUseItem())
            {
                GetComponent<UsableComponent>().Use();
                DropAnyOne();
            }
        }

        public void TryRepairArmorOrWeapon(int value)
        {
            // EquipableComponent нужен только для сортировки
            if (HasComponent<DurabilityComponent>() && HasComponent<EquipableComponent>())
            {
                GetComponent<DurabilityComponent>().Repair(value);
            }
        }
    }
}