using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*        List<Feature> features = new List<Feature>();

                for(int i = 0; i < 2; i++) {
                    Feature feat = new Feature("display name " + i, "source name", "att");
                    feat.NumericValue = i;
                }*/
        ListConverter<FeatureSerializable> listConverter = new ListConverter<FeatureSerializable>();
        DictionaryConverter<FeatureSerializable> dictionaryConverter = new DictionaryConverter<FeatureSerializable>();

        var settings = new JsonSerializerSettings();
        settings.Converters.Add(listConverter);
        settings.Converters.Add(dictionaryConverter);

        Feature feat = new Feature("display name", "source name", "attribute name");
        FeatureSerializable ser = SerializeFeature(feat);

        //List<FeatureSerializable> list = new List<FeatureSerializable>();
        //list.Add(ser);

        Dictionary<string, FeatureSerializable> dict = new Dictionary<string, FeatureSerializable>();
        dict.Add("testKey", ser);


        string featS = JsonConvert.SerializeObject(dict);
        Debug.Log(featS);

        Dictionary<string, FeatureSerializable> des = JsonConvert.DeserializeObject<Dictionary<string, FeatureSerializable>>(featS);
        Feature after = DeserializeFeature(des["testKey"]);
        Debug.Log(after.DisplayName);



        /*       TextAsset txt = Resources.Load<TextAsset>("readme");
               Debug.Log(txt);


               Feature deserialized = JsonConvert.DeserializeObject<Feature>(txt.text);
               Debug.Log(deserialized.AttributeName);

               string serialized = JsonConvert.SerializeObject(deserialized);
               Debug.Log(serialized);*/

    }

    private FeatureSerializable SerializeFeature(Feature feature) {
        FeatureSerializable result = new FeatureSerializable();
        result.DisplayName = feature.DisplayName;
        result.SourceName = feature.SourceName;
        result.AttributeName = feature.AttributeName;
        result.NumericValue = feature.NumericValue;
        result.StringValue = feature.StringValue;
        result.Operation = feature.Operation;
        result.Expiration = feature.Expiration;
        return result;
    }

    private Feature DeserializeFeature(FeatureSerializable featureSerializable) {
        Feature result = new Feature(featureSerializable.DisplayName, featureSerializable.SourceName, featureSerializable.AttributeName);
        result.NumericValue = featureSerializable.NumericValue;
        result.StringValue = featureSerializable.StringValue;
        result.Operation = featureSerializable.Operation;
        result.Expiration = featureSerializable.Expiration;
        return result;
    }

    private class ListConverter<T> : CustomCreationConverter<List<T>> {
        public override List<T> Create(Type objectType) {
            return null;
        }
    }

    private class DictionaryConverter<T> : CustomCreationConverter<Dictionary<string, T>> {
        public override Dictionary<string, T> Create(Type objectType) {
            return null;
        }
    }
}
