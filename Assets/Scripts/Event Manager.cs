using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public List<GameObject> ActEvents;
    public GameObject EventCreater;
    public float EventDistRadius = 10.65f;
    public float StartDistRadius = 20f;
    public GameObject StartEvent;
    public Vector3 EventCursor;
    int first=1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameObject Current;
        EventCursor = StartEvent.transform.position;
        EventCursor = new Vector3 (EventCursor.x, EventCursor.y, EventCursor.z+2*EventDistRadius);
        Current = EventCreater.GetComponent<Creater>().CreatEvent(EventCreater.GetComponent<Creater>().RandSearch());
        Current.transform.position = EventCursor;
        ActEvents.Add(Current);
        EventCursor = new Vector3(EventCursor.x, EventCursor.y, EventCursor.z + 2*EventDistRadius);
        Current = EventCreater.GetComponent<Creater>().CreatEvent(EventCreater.GetComponent<Creater>().RandSearch(Current));
        Current.transform.position = EventCursor;
        ActEvents.Add(Current);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in ActEvents)
        {
            obj.transform.position = new Vector3 (obj.transform.position.x, obj.transform.position.y, obj.transform.position.z-0.01f);
        }
        StartEvent.transform.position = new Vector3(StartEvent.transform.position.x, StartEvent.transform.position.y, StartEvent.transform.position.z - 0.01f);
    }

    public void Reset()
    {
        foreach(GameObject obj in ActEvents)
        {
            ActEvents.Remove(obj);
            Destroy(obj);
            EventCursor = StartEvent.transform.position;
        }
    }

    public void DeleteEvent(GameObject OldEvent)
    {
        foreach(GameObject obj in ActEvents)
        {
            if(obj == OldEvent)
            {
                ActEvents.Remove(obj);
                Destroy(obj);
            }
        }
    }

    public void DestroyLastEvent()
    {
        if (first == 0)
        {
            GameObject a = ActEvents.First();
            ActEvents.Remove(a);
            Destroy(a);
        }
        else
        {
            first--;
        }
    }

    public void AddNewEvent()
    {
        GameObject Current;
        Current = EventCreater.GetComponent<Creater>().RandSearch(ActEvents.Last());
        Current = EventCreater.GetComponent<Creater>().CreatEvent(Current,EventCursor);
        Current.transform.position = ActEvents.Last().transform.position + new Vector3(0,0,2*EventDistRadius);
        ActEvents.Add(Current);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Event")
        {
            Debug.Log("eventi goruyor");
            AddNewEvent();
            DestroyLastEvent();
        }
    }
}
