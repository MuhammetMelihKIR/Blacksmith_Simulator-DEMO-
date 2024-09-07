
  using UnityEngine;

  public interface ITakeable
  {
      void GetObject();
      void GiveObject();
      GameObject GetPrefab();
      BlackSmithObjectSO GetBlackSmithObjectSO();
      void OutlineActive();
  }
