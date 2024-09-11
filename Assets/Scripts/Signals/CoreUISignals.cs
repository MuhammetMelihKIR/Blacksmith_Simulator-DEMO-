using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoreUISignals : MonoBehaviour
  {
      #region Singleton
      private static CoreUISignals _instance;
      public static CoreUISignals Instance { get { return _instance; }
      }

      private void Awake()
      {

          if (_instance != null && _instance != this)
          {
              Destroy(_instance);
              return;
          }
          _instance = this;
        
      }
      #endregion
      
      // Production Table UI
      public static UnityAction<bool> OnProductionTable_CanvasIsActive = delegate { };
      public static UnityAction<bool> OnProductionTable_EquipmentListPanelIsActive = delegate { };
      public static UnityAction<bool> OnProductionTable_ForgingSliderPanelIsActive = delegate { };
      
      // CustomerManager UI
      
      public static UnityAction<string,string> CustomerManager_CustomerInfoUpdate = delegate { };

  }
