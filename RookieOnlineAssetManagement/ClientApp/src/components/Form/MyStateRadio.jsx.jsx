import { Field, useField } from "formik";
import React from "react";
import { Grid } from "semantic-ui-react";
export default function MyStateRadio(prop) {
  const [field, meta, helpers] = useField(prop.name);
  return (
    <>
      <Grid>
        <Grid.Column verticalAlign="middle" width={5}>
          <label>{prop.label}:</label>
        </Grid.Column>
        <Grid.Column verticalAlign="middle" width={11}>
          {prop.option &&
            prop.option.map((opt) => (
              <div className="mt-3" key={opt.value}>
                <Field
                  type="radio"
                  name={prop.name}
                  value={opt.value}
                  onChange={() => helpers.setValue(opt.value)}
                />
                {opt.text}
              </div>
            ))}
        </Grid.Column>
      </Grid>
    </>
  );
}
