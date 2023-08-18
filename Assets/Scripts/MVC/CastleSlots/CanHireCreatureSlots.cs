using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHireCreatureSlots : MonoBehaviour
{
    [SerializeField] private List<HireCreatureSlot> _hireCreatureSlots;
    [SerializeField] private ModelCreatures _modelCreatures;

    
    public void SetSpriteCreature(CastleObjectFullInfo castleFullInfo)
    {
        int i = 0;
        foreach(var creature in castleFullInfo.purchasableCreatureInfoMap)
        {
            foreach(var creatureId in creature.Value.creatureIds)
            {
                _hireCreatureSlots[i].SetIcon(_modelCreatures.GetIconById(creatureId));
                i++;
            }
        }
    }
}
