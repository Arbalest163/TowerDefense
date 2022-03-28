using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyWave : ScriptableObject
{
    [SerializeField]
    private EnemySpawnSequence[] _spawnSequences;

    public State Begin() => new State(this);

    [Serializable]
    public struct State
    {
        private EnemyWave _wave;
        private int _index;
        private EnemySpawnSequence.State _sequence;

        public State(EnemyWave wave)
        {
            _wave = wave;
            _index = 0;
            _sequence = _wave._spawnSequences[0].Begin();
        }

        public float Progress(float deltatime)
        {
            deltatime = _sequence.Progress(deltatime);
            while (deltatime >= 0f)
            {
                if (++_index >= _wave._spawnSequences.Length)
                {
                    return deltatime;
                }

                _sequence = _wave._spawnSequences[_index].Begin();
                deltatime = _sequence.Progress(deltatime);
            }
            return -1f;
        }
    }
}
