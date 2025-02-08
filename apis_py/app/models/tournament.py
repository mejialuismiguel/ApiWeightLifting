from sqlalchemy import Column, Integer, String, Date
from app.database import Base

class Tournament(Base):
    __tablename__ = "tournament"

    id = Column(Integer, primary_key=True, index=True)
    name = Column(String(100))
    location = Column(String(100))
    start_date = Column(Date)
    end_date = Column(Date)