using System;
using System.Collections.Generic;

namespace CircularBuffer
{
    public interface ICircularBuffer<T>
    {
        //Max Anzahl aller Elemente
        int Capacity { get; }
        //Aktuelle Anzahl aller Elemente
        int Count { get; }
        //Ringpuffer ist leer
        bool IsEmpty { get; }
        //Ringpuffer ist voll
        bool IsFull { get; }

        //Fügt ein Element hinzu
        void Produce(T newElement);

        //Entfernt das älteste Element und gibt es zurück
        T Consume();

        //Entfernt alle Elemente
        void Clear();

        //Soll alle Elemente einer Aufzählung hinzufügen, gibt Anzahl der hinzugefügten Elemente wieder
        int ProduceAll(IEnumerable<T> collection);

        //Entfernt alle Element und ruft für jedes eine Funktion mit dem Element als Parameter auf (Action weil kein Rückgabewert)
        void ConsumeAll(Action<T> action);
    }
}