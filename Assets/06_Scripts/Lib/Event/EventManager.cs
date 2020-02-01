using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Reminder - version with EventHandler, if needed one day
 * protected event EventHandler<MessageEventArgs> OnArriveToStation;


public void RegisterOnArriveToStation(Action<object,MessageEventArgs> a_action)
{
    EventHandler<MessageEventArgs> evenHandler = (a_object, a_message) => a_action(a_object, a_message);  
    OnArriveToStation += evenHandler;
}*/


//------------ LIST OF EVENTS -------------//
//NameOfEvent;Param1;Param2

//##BEGIN##
//GiveItem;ItemEventArgs
//GiveRandomItem
//ClientComplete;ClientEventArgs
//Win
//##END##

//------------ END - LIST OF EVENTS -------------//


public class EventManager : Singleton<EventManager>
{

// ----- AUTO GENERATED CODE ----- //




// --- EVENT --- GiveItem --- //


	protected event Action<object, ItemEventArgs> OnGiveItem;
	public void RegisterOnGiveItem(Action<object, ItemEventArgs> a_action)
	{
		OnGiveItem += a_action;
	}


	public void UnRegisterOnGiveItem(Action<object, ItemEventArgs> a_action)
	{
		OnGiveItem -= a_action;
	}


	public void InvokeOnGiveItem(object a_sender, ItemEventArgs a_itemEventArgs)
	{
		if(OnGiveItem != null)
		{
			OnGiveItem.Invoke(a_sender, a_itemEventArgs);
		}
	}




// --- EVENT --- GiveRandomItem --- //


	protected event Action<object> OnGiveRandomItem;
	public void RegisterOnGiveRandomItem(Action<object> a_action)
	{
		OnGiveRandomItem += a_action;
	}


	public void UnRegisterOnGiveRandomItem(Action<object> a_action)
	{
		OnGiveRandomItem -= a_action;
	}


	public void InvokeOnGiveRandomItem(object a_sender)
	{
		if(OnGiveRandomItem != null)
		{
			OnGiveRandomItem.Invoke(a_sender);
		}
	}




// --- EVENT --- ClientComplete --- //


	protected event Action<object, ClientEventArgs> OnClientComplete;
	public void RegisterOnClientComplete(Action<object, ClientEventArgs> a_action)
	{
		OnClientComplete += a_action;
	}


	public void UnRegisterOnClientComplete(Action<object, ClientEventArgs> a_action)
	{
		OnClientComplete -= a_action;
	}


	public void InvokeOnClientComplete(object a_sender, ClientEventArgs a_clientEventArgs)
	{
		if(OnClientComplete != null)
		{
			OnClientComplete.Invoke(a_sender, a_clientEventArgs);
		}
	}




// --- EVENT --- Win --- //


	protected event Action<object> OnWin;
	public void RegisterOnWin(Action<object> a_action)
	{
		OnWin += a_action;
	}


	public void UnRegisterOnWin(Action<object> a_action)
	{
		OnWin -= a_action;
	}


	public void InvokeOnWin(object a_sender)
	{
		if(OnWin != null)
		{
			OnWin.Invoke(a_sender);
		}
	}
// ----- END AUTO GENERATED CODE ----- //

}


public class MessageEventArgs : EventArgs
{
    public string m_message;
    public MessageEventArgs(string a_message)
    {
        m_message = a_message;
    }
}


public class ItemEventArgs : EventArgs
{
    public ThrowableItemType m_itemType;
    public ItemEventArgs(ThrowableItemType a_itemType)
    {
        m_itemType = a_itemType;
    }
}


public class ClientEventArgs : EventArgs
{
    public Client m_client;
    public ClientEventArgs(Client a_client)
    {
        m_client = a_client;
    }
}


