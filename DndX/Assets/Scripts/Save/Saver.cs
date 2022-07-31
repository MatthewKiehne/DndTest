using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : MonoBehaviour
{

    public void LoadInTemplates() {
        TextAsset templatesText = Resources.Load<TextAsset>("Texts/Templates");
        Debug.Log(templatesText);

        Dictionary<string, List<FeatureSerializable>> des = JsonConvert.DeserializeObject<Dictionary<string, List<FeatureSerializable>>>(templatesText.text);

        foreach(KeyValuePair<string, List<FeatureSerializable>> template in des) {

            Actor actor = new Actor();
            foreach(FeatureSerializable fs in template.Value) {
                actor.Features.Add(DeserializeFeature(fs));
            }

            ActorStorage.AddActorToLookUp(template.Key, actor);
        }

        /*        List<Feature> features = new List<Feature>();

                for(int i = 0; i < 2; i++) {
                    Feature feat = new Feature("display name " + i, "source name", "att");
                    feat.NumericValue = i;
                }*/
/*        ListConverter<FeatureSerializable> listConverter = new ListConverter<FeatureSerializable>();
        DictionaryConverter<FeatureSerializable> dictionaryConverter = new DictionaryConverter<FeatureSerializable>();

        var settings = new JsonSerializerSettings();
        settings.Converters.Add(listConverter);
        settings.Converters.Add(dictionaryConverter);

        Feature feat = new Feature("display name", "source name", "attribute name");
        FeatureSerializable ser = SerializeFeature(feat);

        //List<FeatureSerializable> list = new List<FeatureSerializable>();
        //list.Add(ser);

        Dictionary<string, List<FeatureSerializable>> dict = new Dictionary<string, List<FeatureSerializable>>();
        dict.Add("templateName", new List<FeatureSerializable>());
        dict["templateName"].Add(ser);


        string featS = JsonConvert.SerializeObject(dict);
        Debug.Log(featS);

        Dictionary<string, List<FeatureSerializable>> des = JsonConvert.DeserializeObject<Dictionary<string, List<FeatureSerializable>>>(featS);
        List<FeatureSerializable> afterList = des["templateName"];
        Feature f = DeserializeFeature(afterList[0]);
        Debug.Log(f.DisplayName);*/



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
        result.AttributeName = feature.Attribute;
        result.NumericValue = feature.NumericValue;
        result.StringValue = feature.StringValue;
        result.Operation = feature.Operation.ToString();
        result.Expiration = feature.Expiration;
        return result;
    }

    private Feature DeserializeFeature(FeatureSerializable featureSerializable) {
        Feature result = new Feature(featureSerializable.DisplayName, featureSerializable.SourceName, featureSerializable.AttributeName);
        result.NumericValue = featureSerializable.NumericValue;
        result.StringValue = featureSerializable.StringValue;
        result.Operation = (FeatureOperation)Enum.Parse(typeof(FeatureOperation), featureSerializable.Operation, true);
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
