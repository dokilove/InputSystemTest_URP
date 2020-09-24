using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMessage
{
    public string name;
    public BaseMessage() { name = this.GetType().Name; }
}

public delegate bool MessageHandlerDelegate(BaseMessage message);

public class MessagingSystem : SingletonAsComponent<MessagingSystem>
{
    public static MessagingSystem Instance {
        get { return ((MessagingSystem)_Instance); }
        set { _Instance = value; }
    }

    private Dictionary<string, List<MessageHandlerDelegate>> _listenerDict = new Dictionary<string, List<MessageHandlerDelegate>>();
    private Queue<BaseMessage> _messageQue = new Queue<BaseMessage>();
    private float maxQueueProcessingTime = 0.16667f;

    public bool AttachListener(System.Type type, MessageHandlerDelegate handler)
    {
        if (type == null)
        {
            Debug.Log("MessagingSystem: AttachListener failed due to no message type specified");
            return false;
        }

        string msgName = type.Name;
        if (!_listenerDict.ContainsKey(msgName))
        {
            _listenerDict.Add(msgName, new List<MessageHandlerDelegate>());
        }

        List<MessageHandlerDelegate> listenerList = _listenerDict[msgName];
        if (listenerList.Contains(handler))
        {
            Debug.Log("Listener already in list");
            return false;
        }
        listenerList.Add(handler);
        return true;
    }

    public bool QueueMessage(BaseMessage msg)
    {
        if (!_listenerDict.ContainsKey(msg.name))
        {
            return false;
        }
        _messageQue.Enqueue(msg);
        return true;
    }

    private void Update()
    {
        float timer = 0.0f;

        while (_messageQue.Count > 0)
        {
            if (maxQueueProcessingTime > 0.0f)
            {
                if (timer > maxQueueProcessingTime)
                    return;
            }

            BaseMessage msg = _messageQue.Dequeue();
            if (!TriggerMessage(msg))
            {
                Debug.Log("Error when processing message : " + msg.name);
            }

            if (maxQueueProcessingTime > 0.0f)
            {
                timer += Time.deltaTime;
            }
        }
    }

    public bool TriggerMessage(BaseMessage msg)
    {
        string msgName = msg.name;
        if (!_listenerDict.ContainsKey(msgName))
        {
            Debug.Log("MessagingSystem: Message \"" + msgName + "\" has no listeners!");
            return false;
        }

        List<MessageHandlerDelegate> listenerList = _listenerDict[msgName];


        for (int i =0; i< listenerList.Count; ++i)
        {
            if (listenerList[i](msg))
            {
                return true;
            }
        }
        return true;
    }

    public bool DetachListener(System.Type type, MessageHandlerDelegate handler)
    {
        if (type == null)
        {
            Debug.Log("MessagingSystem: DetachListener failed due to no message type specified");
            return false;
        }

        string msgName = type.Name;
        if (!_listenerDict.ContainsKey(type.Name))
        {
            return false;
        }

        List<MessageHandlerDelegate> listenerList = _listenerDict[msgName];
        if (!listenerList.Contains(handler))
        {
            return false;
        }

        listenerList.Remove(handler);
        return true;
    }
}
