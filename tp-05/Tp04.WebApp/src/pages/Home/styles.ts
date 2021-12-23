import styled from 'styled-components'

export const Container = styled.div`
  width: 100%;
  height: 100%;

  .button {
    background: #e1ecf4;

    padding: 0.8rem;
    margin: 1rem;

    border: 0;
    border-radius: 0.4rem;
    text-decoration: none;
    color: inherit;
  }
`

export const Title = styled.div`
  color: #444;
  font-weight: 700;
  font-size: 20px;

  margin: 20px;
`

export const MainContent = styled.main`
  width: 100;
  margin: 20px 40px;

  > button {
    margin-left: auto;
  }
`

export const BookList = styled.table`
  width: 100%;
  background: transparent;
  border-spacing: 0 10px;

  margin: 20px 0;

  thead th {
    text-align: left;
    color: #444444;
    font-size: 14px;
  }

  th,
  td {
    text-align: left;

    &.icon {
      width: 60px;
      text-align: center;

      svg {
        align-self: center;
      }
    }

    button {
      cursor: pointer;
      border: 0;
      background-color: transparent;
    }
  }
`

export const FlexContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
`
