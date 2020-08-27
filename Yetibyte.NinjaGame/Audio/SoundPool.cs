using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.NinjaGame.Audio
{
    public class SoundPool : ICollection<SoundEffect>
    {
        private readonly List<SoundEffect> _sounds = new List<SoundEffect>();

        private readonly Random _random = new Random();
        public int Count => _sounds.Count;

        public bool IsReadOnly => ((ICollection<SoundEffect>)_sounds).IsReadOnly;

        public SoundPool()
        {

        }
        public void Add(SoundEffect item)
        {
            _sounds.Add(item);
        }

        public void Clear()
        {
            _sounds.Clear();
        }

        public bool Contains(SoundEffect item)
        {
            return _sounds.Contains(item);
        }

        public void CopyTo(SoundEffect[] array, int arrayIndex)
        {
            _sounds.CopyTo(array, arrayIndex);
        }

        public IEnumerator<SoundEffect> GetEnumerator()
        {
            return ((ICollection<SoundEffect>)_sounds).GetEnumerator();
        }

        public bool Remove(SoundEffect item)
        {
            return _sounds.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<SoundEffect>)_sounds).GetEnumerator();
        }

        public void PlayRandom(float volume, float pitch, float pan)
        {
            int randomInt = _random.Next(_sounds.Count);
            _sounds[randomInt].Play(volume, pitch, pan);
        }

        public void PlayRandom()
        {
            int randomInt = _random.Next(_sounds.Count);
            _sounds[randomInt].Play();
        }

    }
}
