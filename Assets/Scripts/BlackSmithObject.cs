using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmithObject : MonoBehaviour
{   
    [SerializeField] private BlackSmithObjectSO blackSmithObjectSO;

    public BlackSmithObjectSO GetBlackSmithObjectSO()
    {
        return blackSmithObjectSO;
    }
}
