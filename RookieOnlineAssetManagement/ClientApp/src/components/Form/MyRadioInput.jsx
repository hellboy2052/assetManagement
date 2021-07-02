import { Field, useField } from "formik";
import React from "react";
import { Grid } from "semantic-ui-react";

export default function MyRadioInput(prop) {
  const [field, meta, helpers] = useField(prop.name);
  return (
    <div className="mb-4">
      <Grid>
        <Grid.Column verticalAlign="middle" width={5}>
          <label>{prop.label}:</label>
        </Grid.Column>
        <Grid.Column verticalAlign="middle" width={11}>
          <Grid columns={2} relaxed="very">
            <Grid.Column>
              <Field
                type="radio"
                name={prop.name}
                value={true}
                onChange={() => helpers.setValue(true)}
              />
              Male
            </Grid.Column>
            <Grid.Column>
              <Field
                type="radio"
                name={prop.name}
                value={false}
                onChange={() => helpers.setValue(false)}
              />{" "}
              Female
            </Grid.Column>
          </Grid>
        </Grid.Column>
      </Grid>
    </div>
  );
}
