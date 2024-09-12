
  using UnityEngine;

  public interface ITakeable : IOutlineable
  {
      void GetObject();
      void GiveObject();
      GameObject GetPrefab();
      BlacksmithObjectSO GetBlackSmithObjectSO();
      
  }
