using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SaveInterface {

	void SaveData(UserData data);

    UserData LoadData();
}
