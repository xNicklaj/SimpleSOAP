using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine.Events;

namespace Nicklaj.SimpleSOAP
{
    public class ListVariable<T> : ScriptableVariable<List<T>>, ISaveScriptableData
    {
        public string PersistencyId { get; set; }
        public UnityAction OnItemAdded = delegate { };
        public UnityAction OnItemRemoved = delegate { };
        
        /// <summary>
        /// Adds a new value to the list and invokes the OnItemAdded event.
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            Value.Add(value);
            OnItemAdded.Invoke();
        }

        /// <summary>
        /// Removes a value from the list and invokes the OnItemRemoved event.
        /// </summary>
        /// <param name="value"></param>
        public void Remove(T value)
        {
            Value.Remove(value);
            OnItemRemoved.Invoke();
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(Value);
        }

        public void Deserialize(string data)
        {
            Value = JsonConvert.DeserializeObject<List<T>>(data);
        }
        
        private void Awake()
        {
            if(string.IsNullOrEmpty(PersistencyId))
                PersistencyId = Guid.NewGuid().ToString();
        }
    }

    #region Specific Implementations

    [CreateAssetMenu(fileName = "Game Objects List Variable", menuName = "Simple SOAP/Variables/Lists/Game Objects List")]
    public class GameObjectListVariable : ListVariable<GameObject> { }

    [CreateAssetMenu(fileName = "Integer List Variable", menuName = "Simple SOAP/Variables/Lists/Integer List")]
    public class IntegerListVariable : ListVariable<int> { }

    [CreateAssetMenu(fileName = "Float List Variable", menuName = "Simple SOAP/Variables/Lists/Float List")]
    public class FloatListVariable : ListVariable<float> { }

    [CreateAssetMenu(fileName = "Bool List Variable", menuName = "Simple SOAP/Variables/Lists/Boolean List")]
    public class BoolListVariable : ListVariable<bool> { }

    #endregion
}
// How do I serialize this?