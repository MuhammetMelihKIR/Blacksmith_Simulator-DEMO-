
  using UnityEngine;

  public interface ITakeable
  {
      void GetObject();
      void GiveObject();
      GameObject GetPrefab();
      BlacksmithObjectSO GetBlackSmithObjectSO();
      void OutlineActive();
  }
