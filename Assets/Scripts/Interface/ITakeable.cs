
  using UnityEngine;

  public interface ITakeable
  {
      void GetInteract();
      void GetObject();
      void GiveObject();
      GameObject GetPrefab();
      BlackSmithObjectSO GetBlackSmithObjectSO();
      void OutlineActive();
  }
